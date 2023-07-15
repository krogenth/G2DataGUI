using G2DataGUI.IO.Streams;
using System;
using System.IO;

namespace G2DataGUI.Common.Data.Common;

public class FixedLengthName
{
    private byte[] _characters = new byte[MaxLength];

    public string Name
    {
        get
        {
            string name = "";
            foreach (var character in _characters)
            {
                name += character > 0 ? Convert.ToChar(character) : ' ';
            }

            return name.TrimStart();
        }
        set
        {
            value = value.PadRight(MaxLength, ' ');
            for (int index = 0; index < MaxLength; index++)
            {
                _characters[index] = Convert.ToByte(value[index]);
            }
        }
    }

    public static FixedLengthName ReadFixedLengthName(Stream reader)
    {
        FixedLengthName name = new()
        {
            _characters = reader.ReadRawByteArray(MaxLength),
        };
        return name;
    }

	public void WriteFixedLengthName(Stream writer) =>
		writer.WriteRawByteArray(_characters);

	public static int MaxLength { get => 18; }
}
