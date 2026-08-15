namespace G2DataGUI.Domain.Chr;

/// <summary>
/// One decoded DDS texture. Rgba is always fully decompressed (DXT1/3/5 or
/// uncompressed 32-bit source), so rendering never needs to know which
/// format the file was in. GlobalIdentifier is the value at payload offset
/// 0x20 that these Dreamcast-derived DDS files use to match a texture to
/// a model's GIXL identifier list — see ChrModel.GlobalIdentifiers.
/// </summary>
public sealed class DdsTexture
{
	public string FileName { get; set; } = string.Empty;
	public uint GlobalIdentifier { get; set; }

	public int Width { get; set; }
	public int Height { get; set; }

	public byte[] Rgba { get; set; } = [];
}
