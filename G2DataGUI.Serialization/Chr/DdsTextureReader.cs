using G2DataGUI.Domain.Chr;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Chr;

/// <summary>
/// Thin IDataReader adapter over DdsReader.Read(byte[]), which needs the
/// whole file up front (DXT block decoding isn't naturally streaming) and
/// takes a filename for DdsTexture.FileName — these textures are loaded
/// from a folder of standalone .dds files, not from inside a .chr archive,
/// so the filename is meaningful and worth keeping on the model.
/// </summary>
public sealed class DdsTextureReader(string fileName) : IDataReader<DdsTexture>
{
	public DdsTexture Read(Stream stream)
	{
		using MemoryStream buffer = new();

		stream.CopyTo(buffer);

		return DdsReader.Read(buffer.ToArray(), fileName);
	}

	public async Task<DdsTexture> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
