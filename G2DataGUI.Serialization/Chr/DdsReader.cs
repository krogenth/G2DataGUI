using System.Buffers.Binary;
using System.Numerics;
using System.Text;

using G2DataGUI.Domain.Chr;

namespace G2DataGUI.Serialization.Chr;

public static class DdsReader
{
	//private const uint DdsPfAlphaPixels = 0x1;
	private const uint DdsPfFourCc = 0x4;
	private const uint DdsPfRgb = 0x40;

	public static DdsTexture Read(
		byte[] bytes,
		string fileName = "")
	{
		if (bytes.Length < 128 ||
			Encoding.ASCII.GetString(bytes, 0, 4) != "DDS ")
		{
			throw new InvalidDataException(
				"Not a DDS file.");
		}

		/*
		 * Standard DDS_HEADER layout:
		 *
		 * 0x00  "DDS "
		 * 0x04  dwSize
		 * 0x08  dwFlags
		 * 0x0C  dwHeight
		 * 0x10  dwWidth
		 * 0x14  dwPitchOrLinearSize
		 * ...
		 * 0x4C  ddspf.dwSize
		 * 0x50  ddspf.dwFlags
		 * 0x54  ddspf.dwFourCC
		 * 0x58  ddspf.dwRGBBitCount
		 * 0x5C  ddspf.dwRBitMask
		 * 0x60  ddspf.dwGBitMask
		 * 0x64  ddspf.dwBBitMask
		 * 0x68  ddspf.dwABitMask
		 *
		 * The Dreamcast files additionally use 0x20
		 * for their GIXL/global texture identifier.
		 */

		int height = checked(
			(int)U32(bytes, 0x0C));

		int width = checked(
			(int)U32(bytes, 0x10));

		uint pixelFlags = U32(bytes, 0x50);
		uint fourCcValue = U32(bytes, 0x54);

		string fourCc =
			Encoding.ASCII.GetString(
				bytes,
				0x54,
				4);

		// FIX:
		// dwRGBBitCount is at 0x58, not 0x54.
		uint rgbBitCount = U32(bytes, 0x58);

		uint rMask = U32(bytes, 0x5C);
		uint gMask = U32(bytes, 0x60);
		uint bMask = U32(bytes, 0x64);
		uint aMask = U32(bytes, 0x68);

		/*
		 * Dreamcast texture files store their GIXL identifier
		 * at offset 0x20.
		 */
		uint globalId = U32(bytes, 0x20);

		byte[] rgba = (pixelFlags & DdsPfRgb) != 0 &&
			rgbBitCount == 32
			? Decode32Bit(
				bytes,
				128,
				width,
				height,
				rMask,
				gMask,
				bMask,
				aMask)
			: (pixelFlags & DdsPfFourCc) != 0
				? fourCc switch
				{
					"DXT1" =>
						DecodeDxt1(
							bytes,
							128,
							width,
							height),

					"DXT3" =>
						DecodeDxt3(
							bytes,
							128,
							width,
							height),

					"DXT5" =>
						DecodeDxt5(
							bytes,
							128,
							width,
							height),

					_ =>
						throw new NotSupportedException(
							$"DDS compression '{fourCc}' " +
							$"is not supported.")
				}
				: throw new NotSupportedException(
				"Only 32-bit RGBA and " +
				"DXT1/DXT3/DXT5 DDS textures " +
				"are supported. " +
				$"Flags=0x{pixelFlags:X8}, " +
				$"FourCC='{fourCc}', " +
				$"RGBBitCount={rgbBitCount}.");

		/*
		 * Uncompressed RGB/RGBA DDS.
		 *
		 * Some files use 0x41 for dwFlags:
		 *
		 *	 DDS_PF_RGB | DDS_PF_ALPHAPIXELS
		 *
		 * so don't require the flags to equal exactly 0x40.
		 */

		return new DdsTexture
		{
			FileName = fileName,
			GlobalIdentifier = globalId,
			Width = width,
			Height = height,
			Rgba = rgba
		};
	}

	private static byte[] Decode32Bit(
		byte[] data,
		int offset,
		int width,
		int height,
		uint rMask,
		uint gMask,
		uint bMask,
		uint aMask)
	{
		const int BytesPerPixel = 4;

		int needed = checked(
			width *
			height *
			BytesPerPixel);

		if (offset + needed > data.Length)
		{
			throw new InvalidDataException(
				"DDS pixel data is truncated.");
		}

		var rgba = new byte[needed];

		for (int i = 0;
			 i < width * height;
			 i++)
		{
			uint value =
				U32(data, offset + (i * 4));

			rgba[(i * 4) + 0] =
				Extract(value, rMask);

			rgba[(i * 4) + 1] =
				Extract(value, gMask);

			rgba[(i * 4) + 2] =
				Extract(value, bMask);

			rgba[(i * 4) + 3] =
				aMask == 0
					? (byte)255
					: Extract(value, aMask);
		}

		return rgba;
	}

	private static byte Extract(
		uint value,
		uint mask)
	{
		if (mask == 0)
		{
			return 0;
		}

		int shift =
			BitOperations.TrailingZeroCount(mask);

		uint component =
			(value & mask) >> shift;

		uint maximum =
			mask >> shift;

		return (byte)(
			component * 255 /
			maximum);
	}

	private static byte[] DecodeDxt1(
		byte[] data,
		int offset,
		int width,
		int height)
	{
		var rgba =
			new byte[
				checked(width * height * 4)];

		int blocksWide =
			(width + 3) / 4;

		int blocksHigh =
			(height + 3) / 4;

		int p = offset;

		for (int by = 0;
			 by < blocksHigh;
			 by++)
		{
			for (int bx = 0;
				 bx < blocksWide;
				 bx++)
			{
				Ensure(data, p, 8);

				ushort c0 =
					U16(data, p);

				ushort c1 =
					U16(data, p + 2);

				uint bits =
					U32(data, p + 4);

				p += 8;

				var colors =
					BuildDxtColors(
						c0,
						c1,
						c0 > c1);

				for (int py = 0;
					 py < 4;
					 py++)
				{
					for (int px = 0;
						 px < 4;
						 px++)
					{
						int x =
							(bx * 4) + px;

						int y =
							(by * 4) + py;

						if (x >= width ||
							y >= height)
						{
							continue;
						}

						int colorIndex =
							(int)(
								(bits >>
								 (2 * ((py * 4) + px))) &
								3);

						WritePixel(
							rgba,
							width,
							x,
							y,
							colors[colorIndex]);
					}
				}
			}
		}

		return rgba;
	}

	private static byte[] DecodeDxt3(
		byte[] data,
		int offset,
		int width,
		int height)
	{
		var rgba =
			new byte[
				checked(width * height * 4)];

		int blocksWide =
			(width + 3) / 4;

		int blocksHigh =
			(height + 3) / 4;

		int p = offset;

		for (int by = 0;
			 by < blocksHigh;
			 by++)
		{
			for (int bx = 0;
				 bx < blocksWide;
				 bx++)
			{
				Ensure(data, p, 16);

				ulong alpha =
					BitConverter.ToUInt64(
						data,
						p);

				ushort c0 =
					U16(data, p + 8);

				ushort c1 =
					U16(data, p + 10);

				uint bits =
					U32(data, p + 12);

				p += 16;

				var colors =
					BuildDxtColors(
						c0,
						c1,
						true);

				for (int py = 0;
					 py < 4;
					 py++)
				{
					for (int px = 0;
						 px < 4;
						 px++)
					{
						int x =
							(bx * 4) + px;

						int y =
							(by * 4) + py;

						if (x >= width ||
							y >= height)
						{
							continue;
						}

						int i =
							(py * 4) + px;

						int colorIndex =
							(int)(
								(bits >>
								 (2 * i)) &
								3);

						var color =
							colors[colorIndex];

						color[3] =
							(byte)(
								((alpha >>
								  (4 * i)) &
								 0xF) * 17);

						WritePixel(
							rgba,
							width,
							x,
							y,
							color);
					}
				}
			}
		}

		return rgba;
	}

	private static byte[] DecodeDxt5(
		byte[] data,
		int offset,
		int width,
		int height)
	{
		var rgba =
			new byte[
				checked(width * height * 4)];

		int blocksWide =
			(width + 3) / 4;

		int blocksHigh =
			(height + 3) / 4;

		int p = offset;

		for (int by = 0;
			 by < blocksHigh;
			 by++)
		{
			for (int bx = 0;
				 bx < blocksWide;
				 bx++)
			{
				Ensure(data, p, 16);

				byte a0 = data[p];
				byte a1 = data[p + 1];

				ulong alphaBits = 0;

				for (int i = 0;
					 i < 6;
					 i++)
				{
					alphaBits |=
						(ulong)data[p + 2 + i]
						<< (8 * i);
				}

				ushort c0 =
					U16(data, p + 8);

				ushort c1 =
					U16(data, p + 10);

				uint bits =
					U32(data, p + 12);

				p += 16;

				var colors =
					BuildDxtColors(
						c0,
						c1,
						true);

				var alphas =
					BuildDxt5Alpha(
						a0,
						a1);

				for (int py = 0;
					 py < 4;
					 py++)
				{
					for (int px = 0;
						 px < 4;
						 px++)
					{
						int x =
							(bx * 4) + px;

						int y =
							(by * 4) + py;

						if (x >= width ||
							y >= height)
						{
							continue;
						}

						int i =
							(py * 4) + px;

						int colorIndex =
							(int)(
								(bits >>
								 (2 * i)) &
								3);

						int alphaIndex =
							(int)(
								(alphaBits >>
								 (3 * i)) &
								7);

						var color =
							colors[colorIndex];

						color[3] =
							alphas[alphaIndex];

						WritePixel(
							rgba,
							width,
							x,
							y,
							color);
					}
				}
			}
		}

		return rgba;
	}

	private static byte[][] BuildDxtColors(
		ushort c0,
		ushort c1,
		bool fourColor)
	{
		byte[][] colors =
		[
			Rgb565(c0),
			Rgb565(c1),
			new byte[4],
			new byte[4]
		];

		for (int k = 0; k < 3; k++)
		{
			colors[2][k] =
				(byte)(
					((2 * colors[0][k]) +
					 colors[1][k]) / 3);
		}

		colors[2][3] = 255;

		if (fourColor)
		{
			for (int k = 0; k < 3; k++)
			{
				colors[3][k] =
					(byte)(
						(colors[0][k] +
						 (2 * colors[1][k])) / 3);
			}

			colors[3][3] = 255;
		}
		else
		{
			Array.Clear(colors[2], 0, 3);
			colors[2][3] = 255;

			Array.Clear(colors[3], 0, 4);
		}

		colors[0][3] = 255;
		colors[1][3] = 255;

		return colors;
	}

	private static byte[] Rgb565(
		ushort value)
	{
		return
		[
			(byte)(
				((value >> 11) & 31) *
				255 / 31),

			(byte)(
				((value >> 5) & 63) *
				255 / 63),

			(byte)(
				(value & 31) *
				255 / 31),

			255
		];
	}

	private static byte[] BuildDxt5Alpha(
		byte a0,
		byte a1)
	{
		var a = new byte[8];

		a[0] = a0;
		a[1] = a1;

		if (a0 > a1)
		{
			for (int i = 1;
				 i <= 6;
				 i++)
			{
				a[i + 1] =
					(byte)(
						(((7 - i) * a0) +
						 (i * a1)) / 7);
			}
		}
		else
		{
			for (int i = 1;
				 i <= 4;
				 i++)
			{
				a[i + 1] =
					(byte)(
						(((5 - i) * a0) +
						 (i * a1)) / 5);
			}

			a[6] = 0;
			a[7] = 255;
		}

		return a;
	}

	private static void WritePixel(
		byte[] rgba,
		int width,
		int x,
		int y,
		byte[] color)
	{
		int p =
			((y * width) + x) * 4;

		Buffer.BlockCopy(
			color,
			0,
			rgba,
			p,
			4);
	}

	private static void Ensure(
		byte[] data,
		int offset,
		int count)
	{
		if (offset < 0 ||
			count < 0 ||
			offset + count > data.Length)
		{
			throw new InvalidDataException(
				"DDS pixel data is truncated.");
		}
	}

	private static uint U32(
		byte[] data,
		int offset) =>
		BinaryPrimitives.ReadUInt32LittleEndian(
			data.AsSpan(offset, 4));

	private static ushort U16(
		byte[] data,
		int offset) =>
		BinaryPrimitives.ReadUInt16LittleEndian(
			data.AsSpan(offset, 2));
}
