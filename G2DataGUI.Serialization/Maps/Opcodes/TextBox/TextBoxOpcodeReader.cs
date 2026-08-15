using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

/// <summary>Sub-dispatches on a second byte (TextBoxOption) — direct port
/// of the original ITextBoxOpcode.ReadOpcode.</summary>
public sealed class TextBoxOpcodeReader : IDataReader<IMapOpcode?>
{
	public IMapOpcode? Read(Stream stream)
	{
		byte optionByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(TextBoxOption), optionByte))
		{
			return null;
		}

		return (TextBoxOption)optionByte switch
		{
			TextBoxOption.RemoveTextBox => new RemoveTextBoxOpcodeReader().Read(stream),
			TextBoxOption.CreateTextBox => new CreateTextBoxOpcodeReader().Read(stream),
			TextBoxOption.Unknown1 => new RemoveTextBoxOpcodeReader().Read(stream), // matches the original: same handler as RemoveTextBox
			TextBoxOption.CreateSubTextBox => new CreateSubTextBoxOpcodeReader().Read(stream),
			TextBoxOption.CreateOptionsTextBox => new CreateOptionsTextBoxOpcodeReader().Read(stream),
			TextBoxOption.CreateOverworldTextBox => new CreateOverworldTextBoxOpcodeReader().Read(stream),
			_ => null,
		};
	}

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
