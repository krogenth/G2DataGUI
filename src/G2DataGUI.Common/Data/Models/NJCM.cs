using System.IO;
using System.Linq;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Models;

public class NJCM
{
	public uint ByteLength { get; private set; }
	public NJSBone BaseBone { get; private set; } = null;

	/// <summary>
	/// Represents the ASCII string "NJCM"
	/// </summary>
	public static byte[] Identifier => new byte[] { 0x4E, 0x4A, 0x43, 0x4D };

	/// <summary>
	/// Offsets do not account for Identifier or ByteLength
	/// </summary>
	public static long DataOffset => 0x08;

	public static NJCM ReadNJCM(Stream reader, long instanceOffset)
	{
		var identifier = reader.ReadRawByteArray((uint)Identifier.Length);
		if (!Identifier.SequenceEqual(identifier))
		{
			return null;
		}

		NJCM njcm = new()
		{
			ByteLength = reader.ReadRawUInt(),
			BaseBone = NJSBone.ReadNJSObject(reader, instanceOffset + DataOffset),
		};

		return njcm;
	}
}
