using System;
using System.Collections.Generic;
using System.Numerics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using G2DataGUI.Domain.Chr;
using G2DataGUI.Domain.Models;

namespace G2DataGUI.Rendering;

/// <summary>
/// Ported from DreamcastChrViewer's ModelViewport, with one structural
/// change to match this project's Domain split: the original cached
/// pre-skinned world-space Vertices/Normals directly on NjModel and
/// recomputed them via ApplyPose/ResetPose whenever the pose changed.
/// Here NjModel only ever holds bone-local data (see NjModel's own doc
/// comment), so this control computes bone world transforms and skins
/// every vertex fresh on each Render call via NjPoseEvaluator — bind pose
/// when Motion is null, the given Frame of Motion otherwise. Everything
/// else (projection, the painter's-algorithm sort, the depth-tested
/// rasterizers, bilinear filtering) is unchanged from the fixes verified
/// earlier in this project's development — see PORTING-Chr.md.
///
/// Not wired to any view/window yet — a control on its own, set up the
/// same way DreamcastChrViewer's MainWindow used to: construct one, call
/// SetModel, add it to a visual tree.
/// </summary>
public sealed class ModelViewport : Control
{
	private float _yaw = MathF.PI;
	private float _pitch = 0.25f;
	private float _distance = 1.8f;

	private Point _lastMouse;
	private bool _dragging;

	private WriteableBitmap? _textureCanvas;
	private byte[]? _pixelBuffer;
	private float[]? _depthBuffer;
	private int _pixelWidth;
	private int _pixelHeight;

	public NjModel? Model { get; private set; }
	public IReadOnlyList<DdsTexture?> Textures { get; private set; } = [];

	/// <summary>Null means render the bind pose. Set alongside Frame via
	/// SetAnimation — see its doc comment for why Motion and Frame are
	/// always changed together rather than as two independent setters.</summary>
	public NjMotion? Motion { get; private set; }

	public float Frame { get; private set; }

	public bool Wireframe { get; set; }
	public bool Points { get; set; }
	public bool Textured { get; set; } = true;

	public event EventHandler? ModelChanged;

	public ModelViewport()
	{
		Focusable = true;

		PointerPressed += OnPointerPressed;
		PointerReleased += OnPointerReleased;
		PointerMoved += OnPointerMoved;
		PointerWheelChanged += OnWheel;
	}

	public void SetModel(
		NjModel? model,
		IReadOnlyList<DdsTexture?>? textures = null)
	{
		Model = model;
		Textures = textures ?? [];
		Motion = null;
		Frame = 0f;
		ResetCamera();
		ModelChanged?.Invoke(this, EventArgs.Empty);
		InvalidateVisual();
	}

	/// <summary>Sets (or clears, with motion: null) which animation frame
	/// to render. Motion and Frame are set together deliberately — a
	/// frame number only means something relative to a specific NjMotion
	/// (its own FrameCount, its own tracks), so there's no valid
	/// "change just the frame" call once a different motion is playing;
	/// callers driving playback call this every tick with both.</summary>
	public void SetAnimation(NjMotion? motion, float frame)
	{
		Motion = motion;
		Frame = frame;
		InvalidateVisual();
	}

	public void Refresh() => InvalidateVisual();

	public void ResetCamera()
	{
		// NJ/Noesis convention presents the character from -Z.
		// Start there so the default view matches the reference viewer.
		_yaw = MathF.PI;
		_pitch = 0.25f;
		_distance = 1.8f;
		InvalidateVisual();
	}

	public override void Render(DrawingContext ctx)
	{
		base.Render(ctx);

		ctx.FillRectangle(Brushes.Black, Bounds);

		if (Model == null || Model.Vertices.Count == 0)
		{
			return;
		}

		Matrix4x4[] worldTransforms =
			Motion is not null
				? NjPoseEvaluator.ComputeAnimatedWorldTransforms(Model, Motion, Frame)
				: NjPoseEvaluator.ComputeBindWorldTransforms(Model);

		Vector3 center = Model.Center;
		float radius = Math.Max(Model.Radius, 0.001f);

		Matrix4x4 rot =
			Matrix4x4.CreateRotationX(_pitch) *
			Matrix4x4.CreateRotationY(_yaw);

		var projected = new Point[Model.Vertices.Count];
		var depth = new float[Model.Vertices.Count];

		for (int i = 0; i < Model.Vertices.Count; i++)
		{
			NjVertex vertex = Model.Vertices[i];

			Vector3 worldPosition = NjPoseEvaluator.SkinPosition(vertex, worldTransforms);

			Vector3 p = Vector3.Transform(
				worldPosition - center,
				rot);

			p.Z += (radius * _distance) + (radius * 1.8f);

			float f = (float)(Bounds.Height * 0.8 /
				Math.Max(p.Z, 0.01f));

			projected[i] = new Point(
				(Bounds.Width * 0.5) + (p.X * f),
				(Bounds.Height * 0.5) - (p.Y * f));

			depth[i] = p.Z;
		}

		var triangleIds = new List<(NjTriangle T, float Z)>();

		foreach (var t in Model.Triangles)
		{
			triangleIds.Add((t,
				(depth[t.A] + depth[t.B] + depth[t.C]) / 3));
		}

		// Painter's algorithm no longer needs to be correct now that the
		// rasterizers below depth-test every pixel — this sort just
		// reduces overdraw a little.
		triangleIds.Sort((x, y) => y.Z.CompareTo(x.Z));

		if (Textured && !Wireframe)
		{
			EnsureTextureCanvas(
				Math.Max(1, (int)Math.Ceiling(Bounds.Width)),
				Math.Max(1, (int)Math.Ceiling(Bounds.Height)));

			ClearPixelBuffer();

			foreach (var (t, z) in triangleIds)
			{
				DdsTexture? texture =
					t.TextureIndex >= 0 &&
					t.TextureIndex < Textures.Count
						? Textures[t.TextureIndex]
						: null;

				if (texture != null && t.HasUv)
				{
					RasterizeTexturedTriangle(
						projected[t.A],
						projected[t.B],
						projected[t.C],
						depth[t.A],
						depth[t.B],
						depth[t.C],
						t.UvA,
						t.UvB,
						t.UvC,
						t.TextureFlags,
						texture);
				}
				else
				{
					RasterizeSolidTriangle(
						projected[t.A],
						projected[t.B],
						projected[t.C],
						depth[t.A],
						depth[t.B],
						depth[t.C],
						Color.FromArgb(255, 180, 185, 195));
				}
			}

			FlushPixelBuffer();

			if (_textureCanvas != null)
			{
				ctx.DrawImage(
					_textureCanvas,
					new Rect(0, 0,
						_textureCanvas.PixelSize.Width,
						_textureCanvas.PixelSize.Height),
					new Rect(0, 0,
						Bounds.Width,
						Bounds.Height));
			}
		}
		else
		{
			var fill =
				new SolidColorBrush(
					Color.FromArgb(255, 180, 185, 195));

			foreach (var (t, z) in triangleIds)
			{
				DrawTriangle(
					ctx,
					projected[t.A],
					projected[t.B],
					projected[t.C],
					Wireframe ? null : fill);
			}
		}

		// Always draw the wireframe over the textured/solid result when enabled.
		if (Wireframe)
		{
			var wirePen = new Pen(
				Brushes.White,
				1);

			foreach (var (t, z) in triangleIds)
			{
				ctx.DrawLine(wirePen,
					projected[t.A], projected[t.B]);
				ctx.DrawLine(wirePen,
					projected[t.B], projected[t.C]);
				ctx.DrawLine(wirePen,
					projected[t.C], projected[t.A]);
			}
		}

		if (Points || Model.Triangles.Count == 0)
		{
			foreach (Point p in projected)
			{
				ctx.DrawEllipse(
					Brushes.White,
					null,
					p,
					1.5,
					1.5);
			}
		}

		DrawAxis(ctx, rot);
	}

	private static void DrawTriangle(
		DrawingContext ctx,
		Point a,
		Point b,
		Point c,
		IBrush? fill)
	{
		var geometry = new StreamGeometry();

		using (var g = geometry.Open())
		{
			g.BeginFigure(a, fill != null);
			g.LineTo(b);
			g.LineTo(c);
			g.EndFigure(true);
		}

		ctx.DrawGeometry(
			fill,
			new Pen(Brushes.White, 1),
			geometry);
	}

	private void EnsureTextureCanvas(int width, int height)
	{
		if (_textureCanvas != null &&
			_pixelWidth == width &&
			_pixelHeight == height)
		{
			return;
		}

		_textureCanvas?.Dispose();

		_pixelWidth = width;
		_pixelHeight = height;
		_pixelBuffer = new byte[checked(width * height * 4)];
		_depthBuffer = new float[checked(width * height)];

		_textureCanvas = new WriteableBitmap(
			new PixelSize(width, height),
			new Avalonia.Vector(96, 96),
			PixelFormat.Bgra8888,
			AlphaFormat.Opaque);
	}

	private void ClearPixelBuffer()
	{
		if (_pixelBuffer == null)
		{
			return;
		}

		// Black background, opaque.
		for (int i = 0; i < _pixelBuffer.Length; i += 4)
		{
			_pixelBuffer[i + 0] = 0;
			_pixelBuffer[i + 1] = 0;
			_pixelBuffer[i + 2] = 0;
			_pixelBuffer[i + 3] = 255;
		}

		// Everything starts "infinitely far" so the first triangle to
		// cover a pixel always passes the depth test.
		if (_depthBuffer != null)
		{
			Array.Fill(_depthBuffer, float.PositiveInfinity);
		}
	}

	private void FlushPixelBuffer()
	{
		if (_textureCanvas == null || _pixelBuffer == null)
		{
			return;
		}

		using var fb = _textureCanvas.Lock();

		int sourceStride = _pixelWidth * 4;

		for (int y = 0; y < _pixelHeight; y++)
		{
			System.Runtime.InteropServices.Marshal.Copy(
				_pixelBuffer,
				y * sourceStride,
				fb.Address + (y * fb.RowBytes),
				sourceStride);
		}
	}

	private void RasterizeSolidTriangle(
		Point a,
		Point b,
		Point c,
		float depthA,
		float depthB,
		float depthC,
		Color color)
	{
		if (_pixelBuffer == null)
		{
			return;
		}

		int minX = Math.Max(0,
			(int)Math.Floor(Math.Min(a.X, Math.Min(b.X, c.X))));
		int maxX = Math.Min(_pixelWidth - 1,
			(int)Math.Ceiling(Math.Max(a.X, Math.Max(b.X, c.X))));
		int minY = Math.Max(0,
			(int)Math.Floor(Math.Min(a.Y, Math.Min(b.Y, c.Y))));
		int maxY = Math.Min(_pixelHeight - 1,
			(int)Math.Ceiling(Math.Max(a.Y, Math.Max(b.Y, c.Y))));

		double area = Edge(a, b, c);
		if (Math.Abs(area) < 0.000001)
		{
			return;
		}

		for (int y = minY; y <= maxY; y++)
		{
			for (int x = minX; x <= maxX; x++)
			{
				Point p = new(x + 0.5, y + 0.5);

				double w0 = Edge(b, c, p) / area;
				double w1 = Edge(c, a, p) / area;
				double w2 = Edge(a, b, p) / area;

				if (w0 < 0 || w1 < 0 || w2 < 0)
				{
					continue;
				}

				if (!TryDepthTest(x, y, w0, w1, w2, depthA, depthB, depthC))
				{
					continue;
				}

				WritePixel(x, y,
					color.R,
					color.G,
					color.B,
					color.A);
			}
		}
	}

	private void RasterizeTexturedTriangle(
		Point a,
		Point b,
		Point c,
		float depthA,
		float depthB,
		float depthC,
		Vector2 uvA,
		Vector2 uvB,
		Vector2 uvC,
		byte textureFlags,
		DdsTexture texture)
	{
		if (_pixelBuffer == null ||
			texture.Width <= 0 ||
			texture.Height <= 0 ||
			texture.Rgba.Length < texture.Width * texture.Height * 4)
		{
			return;
		}

		int minX = Math.Max(0,
			(int)Math.Floor(Math.Min(a.X, Math.Min(b.X, c.X))));
		int maxX = Math.Min(_pixelWidth - 1,
			(int)Math.Ceiling(Math.Max(a.X, Math.Max(b.X, c.X))));
		int minY = Math.Max(0,
			(int)Math.Floor(Math.Min(a.Y, Math.Min(b.Y, c.Y))));
		int maxY = Math.Min(_pixelHeight - 1,
			(int)Math.Ceiling(Math.Max(a.Y, Math.Max(b.Y, c.Y))));

		double area = Edge(a, b, c);
		if (Math.Abs(area) < 0.000001)
		{
			return;
		}

		for (int y = minY; y <= maxY; y++)
		{
			for (int x = minX; x <= maxX; x++)
			{
				Point p = new(x + 0.5, y + 0.5);

				double w0 = Edge(b, c, p) / area;
				double w1 = Edge(c, a, p) / area;
				double w2 = Edge(a, b, p) / area;

				if (w0 < 0 || w1 < 0 || w2 < 0)
				{
					continue;
				}

				if (!TryDepthTest(x, y, w0, w1, w2, depthA, depthB, depthC))
				{
					continue;
				}

				float u = (float)(
					(uvA.X * w0) +
					(uvB.X * w1) +
					(uvC.X * w2));

				float v = (float)(
					(uvA.Y * w0) +
					(uvB.Y * w1) +
					(uvC.Y * w2));

				// NJ UVs are already in the same orientation as the
				// decoded DDS buffer for these CHR files.  Do not
				// unconditionally flip V: the Tiny chunk explicitly
				// carries per-material U/V flip flags.
				if ((textureFlags & 0x20) != 0)
				{
					u = 1.0f - u;
				}

				if ((textureFlags & 0x40) != 0)
				{
					v = 1.0f - v;
				}

				bool clampU =
					(textureFlags & 0x08) != 0;

				bool clampV =
					(textureFlags & 0x10) != 0;

				u = clampU
					? Math.Clamp(u, 0.0f, 1.0f)
					: Wrap(u);

				v = clampV
					? Math.Clamp(v, 0.0f, 1.0f)
					: Wrap(v);

				Color sampled = SampleTextureBilinear(
					texture,
					u,
					v,
					clampU,
					clampV);

				WritePixel(
					x, y,
					sampled.R,
					sampled.G,
					sampled.B,
					sampled.A);
			}
		}
	}

	/// <summary>
	/// Bilinear texture sample — blends the 4 nearest texels instead of
	/// picking one. Point (nearest-neighbor) sampling is the difference
	/// most likely to explain why acute/converging geometry (like the
	/// collar's V-point) looks noticeably blockier here than in Neosis,
	/// which very likely filters.
	/// </summary>
	private static Color SampleTextureBilinear(
		DdsTexture texture,
		float u,
		float v,
		bool clampU,
		bool clampV)
	{
		// Standard half-texel-centered bilinear: texel (0,0)'s center is
		// at u=0.5/width, so subtracting 0.5 before flooring gives the
		// correct pair of texels to blend between at any u.
		float fx = (u * texture.Width) - 0.5f;
		float fy = (v * texture.Height) - 0.5f;

		int x0 = (int)MathF.Floor(fx);
		int y0 = (int)MathF.Floor(fy);

		float tx = fx - x0;
		float ty = fy - y0;

		(byte r00, byte g00, byte b00, byte a00) = SampleTexel(texture, x0, y0, clampU, clampV);
		(byte r10, byte g10, byte b10, byte a10) = SampleTexel(texture, x0 + 1, y0, clampU, clampV);
		(byte r01, byte g01, byte b01, byte a01) = SampleTexel(texture, x0, y0 + 1, clampU, clampV);
		(byte r11, byte g11, byte b11, byte a11) = SampleTexel(texture, x0 + 1, y0 + 1, clampU, clampV);

		byte r = Blerp(r00, r10, r01, r11, tx, ty);
		byte g = Blerp(g00, g10, g01, g11, tx, ty);
		byte b = Blerp(b00, b10, b01, b11, tx, ty);
		byte a = Blerp(a00, a10, a01, a11, tx, ty);

		return Color.FromArgb(a, r, g, b);
	}

	private static byte Blerp(
		byte c00,
		byte c10,
		byte c01,
		byte c11,
		float tx,
		float ty)
	{
		float top = c00 + ((c10 - c00) * tx);
		float bottom = c01 + ((c11 - c01) * tx);
		float value = top + ((bottom - top) * ty);

		return (byte)Math.Clamp(
			MathF.Round(value),
			0,
			255);
	}

	private static (byte R, byte G, byte B, byte A) SampleTexel(
		DdsTexture texture,
		int tx,
		int ty,
		bool clampU,
		bool clampV)
	{
		tx = WrapTexelCoord(tx, texture.Width, clampU);
		ty = WrapTexelCoord(ty, texture.Height, clampV);

		int source = ((ty * texture.Width) + tx) * 4;

		return (
			texture.Rgba[source + 0],
			texture.Rgba[source + 1],
			texture.Rgba[source + 2],
			texture.Rgba[source + 3]);
	}

	private static int WrapTexelCoord(int coord, int size, bool clamp)
	{
		if (clamp)
		{
			return Math.Clamp(coord, 0, size - 1);
		}

		int wrapped = coord % size;
		return wrapped < 0 ? wrapped + size : wrapped;
	}

	private static float Wrap(float value)
	{
		value -= MathF.Floor(value);
		return value;
	}

	private void WritePixel(
		int x,
		int y,
		byte r,
		byte g,
		byte b,
		byte a)
	{
		if (_pixelBuffer == null ||
			x < 0 || y < 0 ||
			x >= _pixelWidth ||
			y >= _pixelHeight)
		{
			return;
		}

		int p = ((y * _pixelWidth) + x) * 4;

		// WriteableBitmap uses BGRA ordering.
		_pixelBuffer[p + 0] = b;
		_pixelBuffer[p + 1] = g;
		_pixelBuffer[p + 2] = r;
		_pixelBuffer[p + 3] = a;
	}

	/// <summary>
	/// Per-pixel depth test, replacing the old "whichever triangle drew
	/// last wins" behavior. w0/w1/w2 are the same barycentric weights the
	/// caller already computed for the inside-triangle test, reused here
	/// to interpolate depth at this exact pixel rather than relying on
	/// one average depth per triangle (the painter's-algorithm sort still
	/// runs beforehand, but no longer needs to be correct — with a real
	/// depth test, draw order can no longer produce a wrong result, only
	/// waste a little time on pixels that get overwritten by something
	/// closer). Smaller depth means closer to the camera — see how `depth`
	/// is built in Render.
	/// </summary>
	private bool TryDepthTest(
		int x,
		int y,
		double w0,
		double w1,
		double w2,
		float depthA,
		float depthB,
		float depthC)
	{
		if (_depthBuffer == null ||
			x < 0 || y < 0 ||
			x >= _pixelWidth ||
			y >= _pixelHeight)
		{
			// No depth buffer available — fall back to the old
			// draw-order-only behavior rather than blocking every pixel.
			return true;
		}

		float depth = (float)(
			(depthA * w0) +
			(depthB * w1) +
			(depthC * w2));

		int index = (y * _pixelWidth) + x;

		if (depth >= _depthBuffer[index])
		{
			return false;
		}

		_depthBuffer[index] = depth;
		return true;
	}

	private static double Edge(Point a, Point b, Point p) =>
		((p.X - a.X) * (b.Y - a.Y)) -
		((p.Y - a.Y) * (b.X - a.X));

	private static void DrawAxis(
		DrawingContext ctx,
		Matrix4x4 rot)
	{
		var origin = new Point(60, 60);

		DrawAxisLine(ctx, origin,
			Vector3.Transform(Vector3.UnitX, rot),
			Brushes.Red);

		DrawAxisLine(ctx, origin,
			Vector3.Transform(Vector3.UnitY, rot),
			Brushes.LimeGreen);

		DrawAxisLine(ctx, origin,
			Vector3.Transform(Vector3.UnitZ, rot),
			Brushes.DodgerBlue);
	}

	private static void DrawAxisLine(
		DrawingContext ctx,
		Point origin,
		Vector3 v,
		IBrush brush)
	{
		var end = origin +
			new Avalonia.Vector(v.X * 28, -v.Y * 28);

		ctx.DrawLine(
			new Pen(brush, 2),
			origin,
			end);
	}

	private void OnPointerPressed(
		object? sender,
		PointerPressedEventArgs e)
	{
		if (!e.GetCurrentPoint(this)
			.Properties.IsLeftButtonPressed)
		{
			return;
		}

		_dragging = true;
		_lastMouse = e.GetPosition(this);
		e.Pointer.Capture(this);
	}

	private void OnPointerReleased(
		object? sender,
		PointerReleasedEventArgs e)
	{
		_dragging = false;
		e.Pointer.Capture(null);
	}

	private void OnPointerMoved(
		object? sender,
		PointerEventArgs e)
	{
		if (!_dragging)
		{
			return;
		}

		Point p = e.GetPosition(this);
		Avalonia.Vector delta = p - _lastMouse;
		_lastMouse = p;

		_yaw += (float)delta.X * 0.01f;
		_pitch += (float)delta.Y * 0.01f;

		_pitch = Math.Clamp(_pitch, -1.5f, 1.5f);

		InvalidateVisual();
	}

	private void OnWheel(
		object? sender,
		PointerWheelEventArgs e)
	{
		_distance *= e.Delta.Y > 0
			? 0.88f
			: 1.14f;

		_distance = Math.Clamp(
			_distance, 0.2f, 12f);

		InvalidateVisual();
	}
}
