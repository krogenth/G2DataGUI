using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Maps;
using G2DataGUI.Serialization.Maps.Opcodes;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

/// <summary>
/// Shared loop for every ITextBoxOpcode variant except Remove: read a byte,
/// and either (a) it's a defined DialogueOpcode — dispatch it, add the
/// result, and stop once that result is itself an ITextBoxOpcode
/// (terminator), or (b) it isn't — back up and read it as text instead.
/// CreateOptionsTextBoxOpcode additionally treats a raw 0x02 byte as its
/// own terminator (extraTerminatorByte) — every other variant passes null.
/// </summary>
internal static class TextBoxNestedOpcodeReader
{
	public static List<IMapOpcode> ReadUntilTerminator(Stream stream, byte? extraTerminatorByte)
	{
		List<IMapOpcode> nested = [];

		while (true)
		{
			byte data = stream.ReadRawByte();

			if (Enum.IsDefined(typeof(DialogueOpcode), data))
			{
				IMapOpcode? nestedOpcode = MapOpcodeReader.ReadFromOpcode(stream, (DialogueOpcode)data);

				if (nestedOpcode is not null)
				{
					nested.Add(nestedOpcode);
				}

				if (nestedOpcode is ITextBoxOpcode)
				{
					return nested;
				}
			}
			else if (extraTerminatorByte is not null && data == extraTerminatorByte)
			{
				return nested;
			}
			else
			{
				stream.Seek(-1, SeekOrigin.Current);
				nested.Add(new TextOpcodeReader().Read(stream));
			}
		}
	}
}
