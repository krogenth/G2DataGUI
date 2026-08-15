namespace G2DataGUI.Domain.Abstractions;

/// <summary>
/// Implemented by any domain model that a Repository can look up by key.
/// For formats with no real on-disk id, TId is usually int and the id is
/// just the item's position in the file (assigned by the reader/repository,
/// never read from the bytes) — see MoveReader for an example.
/// </summary>
public interface IIdentifiable<TId>
{
	TId Id { get; set; }
}
