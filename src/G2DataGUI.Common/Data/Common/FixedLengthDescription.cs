using System;
using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Common;

public class FixedLengthDescription
{
	private byte[] _characters = new byte[MaxLength];

	public string Description
	{
		get
		{
			string description = "";
			foreach (var character in _characters)
			{
				description += Convert.ToChar(character);
			}

			return description.TrimEnd();
		}
		set
		{
            value = value.PadRight(MaxLength, ' ');
			for (int index = 0; index < MaxLength; index++)
			{
                _characters[index] = Convert.ToByte(_characters[index]);
			}
        }
	}

	public static FixedLengthDescription ReadFixedLengthDescription(Stream reader)
	{
		FixedLengthDescription description = new()
		{
            _characters = reader.ReadRawByteArray(MaxLength),
		};
		return description;
	}

	public void WriteFixedLengthDescription(Stream writer)
	{
		writer.WriteRawByteArray(_characters);
	}

    public static int MaxLength { get => 40; }
}