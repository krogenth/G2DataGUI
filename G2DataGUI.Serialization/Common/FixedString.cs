using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Common;

/// <summary>
/// Fixed-width, space-padded ASCII strings, as used for names and
/// descriptions across most of the binary formats. Replaces the old
/// FixedLengthName/FixedLengthDescription types — those were serialization
/// detail that had leaked into the domain models as string-like types.
/// Names are left-padded and read with TrimStart (matches FixedLengthName);
/// descriptions are right-padded and read with TrimEnd (matches
/// FixedLengthDescription). Get the padding side right per-field or values
/// will silently round-trip with the wrong-side whitespace trimmed.
/// </summary>
public static class FixedString
{
	public static string ReadLeftPadded(Stream stream, int length)
	{
		byte[] bytes = stream.ReadRawByteArray((uint)length);
		return DecodeBytes(bytes).TrimStart();
	}

	public static string ReadRightPadded(Stream stream, int length)
	{
		byte[] bytes = stream.ReadRawByteArray((uint)length);
		return DecodeBytes(bytes).TrimEnd();
	}

	public static void WriteLeftPadded(Stream stream, string value, int length) =>
		stream.WriteRawByteArray(EncodeBytes(value.PadLeft(length, ' '), length));

	public static void WriteRightPadded(Stream stream, string value, int length) =>
		stream.WriteRawByteArray(EncodeBytes(value.PadRight(length, ' '), length));

	private static string DecodeBytes(byte[] bytes)
	{
		char[] chars = new char[bytes.Length];

		for (int i = 0; i < bytes.Length; i++)
		{
			chars[i] = bytes[i] > 0 ? (char)bytes[i] : ' ';
		}

		return new string(chars);
	}

	private static byte[] EncodeBytes(string padded, int length)
	{
		byte[] bytes = new byte[length];

		for (int i = 0; i < length; i++)
		{
			bytes[i] = (byte)padded[i];
		}

		return bytes;
	}
}
