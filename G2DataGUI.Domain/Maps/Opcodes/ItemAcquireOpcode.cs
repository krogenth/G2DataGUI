namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Adds items to inventory. Domain type name is spelled correctly;
/// the DialogueOpcode enum member (ItemAquire) keeps the original's typo —
/// see the note on DialogueOpcode.</summary>
public sealed class ItemAcquireOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.ItemAquire;

	public byte Unknown1 { get; set; }

	/// <summary>Index into Items — resolvable via ItemRepository.Find,
	/// same convention as EnemyStats.Item1Offset.</summary>
	public byte Item1Offset { get; set; }
}
