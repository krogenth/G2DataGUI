namespace G2DataGUI.Serialization.Abstractions;

/// <summary>
/// Reads exactly one T positioned at the stream's current location. For a
/// format where "one record" is meaningful on its own — a single opcode in
/// a dialogue stream, a single Enemy at a known pointer offset, a single
/// Item header within a larger file — implement this instead of
/// ICollectionDataReader. A type can implement both when it's genuinely
/// used both ways (see ItemReader: one Item at a time when resolving a
/// pointer, or the whole ITEM.BIN as a collection).
/// </summary>
public interface IDataReader<T>
{
	T Read(Stream stream);

	Task<T> ReadAsync(Stream stream, CancellationToken cancellationToken = default);
}
