using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

public sealed class CreateOptionsTextBoxOpcodeReader : IDataReader<CreateOptionsTextBoxOpcode>
{
	/// <summary>
	/// Only this variant treats a raw 0x02 byte as an additional
	/// terminator, matching the original's extra `else if (data == 0x02)` branch.
	/// </summary>
	private const byte OptionsTerminatorByte = 0x02;

	public CreateOptionsTextBoxOpcode Read(Stream stream)
	{
		CreateOptionsTextBoxOpcode opcode = new()
		{
			TextBoxLength = stream.ReadRawByte(),
			TextBoxHeight = stream.ReadRawByte(),
			NestedOpcodes = TextBoxNestedOpcodeReader.ReadUntilTerminator(stream, OptionsTerminatorByte)
		};

		return opcode;
	}

	public async Task<CreateOptionsTextBoxOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
