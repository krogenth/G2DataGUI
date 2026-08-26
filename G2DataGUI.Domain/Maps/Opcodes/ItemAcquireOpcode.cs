namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Adds items to inventory. Domain type name is spelled correctly;
/// the DialogueOpcode enum member (ItemAquire) keeps the original's typo —
/// see the note on DialogueOpcode.
///
/// `11 01 0c 00 0b...` (Unknown1=0x01, Item1Offset=0x0c, Unknown2=0x00),
/// immediately followed by a valid CameraMove. Observed values for
/// Unknown2 are small (0x00-0x03) — a quantity or flag is plausible but
/// unconfirmed, hence the placeholder name rather than a guess.
/// </summary>
public sealed class ItemAcquireOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.ItemAcquire;

	public byte Unknown1 { get; set; }

	/// <summary>
	/// Index into Items — resolvable via ItemRepository.Find,
	/// same convention as EnemyStats.Item1Offset.
	/// </summary>
	public byte Item1Offset { get; set; }

	public byte Unknown2 { get; set; }
}
