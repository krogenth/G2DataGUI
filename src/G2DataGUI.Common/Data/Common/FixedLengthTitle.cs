using System;
using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Common;
public class FixedLengthTitle
{
	private byte[] _characters = new byte[MaxLength];

	public string Title
	{
		get
		{
			string title = "";
			foreach (var character in _characters)
			{
				title += character > 0 ? Convert.ToChar(character) : ' ';
			}

			return title.TrimStart();
		}
		set
		{
			value = value.PadRight((int)MaxLength, ' ');
			for (int index = 0; index < MaxLength; index++)
			{
				_characters[index] = Convert.ToByte(value[index]);
			}
		}
	}

	public static FixedLengthTitle ReadFixedLengthTitle(Stream reader)
	{
		FixedLengthTitle title = new()
		{
			_characters = reader.ReadRawByteArray(MaxLength),
		};
		return title;
	}

	public void WriteFixedLengthTitle(Stream writer) =>
		writer.WriteRawByteArray(_characters);

	public static uint MaxLength { get => 8; }
}
