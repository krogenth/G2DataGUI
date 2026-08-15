using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Items;

public sealed class Item : IIdentifiable<int>
{
	public const int MaxNameLength = 18;
	public const int MaxDescriptionLength = 40;

	/// <summary>
	/// Position within ITEM.BIN. The on-disk id field exists too
	/// (see ItemReader/ItemWriter) but is discarded/rewritten from this,
	/// same convention as Move.Id — see PORTING.md.
	/// </summary>
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public ItemStats Stats { get; set; } = new();

	/// <summary>
	/// Null when this item has no equipment block — see Equipment.
	/// </summary>
	public Equipment? Equipment { get; set; }

	/// <summary>
	/// Null when this item has no usable block — see Usable.
	/// </summary>
	public Usable? Usable { get; set; }
}
