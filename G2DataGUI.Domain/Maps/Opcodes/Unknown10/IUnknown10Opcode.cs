namespace G2DataGUI.Domain.Maps.Opcodes.Unknown10;

/// <summary>
/// Completely unknown, no clue on parameters. Sub-dispatches on a
/// second byte (Unknown10Option); no recursion.
/// </summary>
public interface IUnknown10Opcode : IMapOpcode
{
	Unknown10Option Option { get; }
}
