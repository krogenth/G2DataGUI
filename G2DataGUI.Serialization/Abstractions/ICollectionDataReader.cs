namespace G2DataGUI.Serialization.Abstractions;

/// <summary>
/// Reads every record a stream holds — the shape every whole-file binary
/// format used (MS_PARAM.BIN, SK_PARAM.BIN, ...) before this split existed.
/// Repository<T,TId> is built against this, not IDataReader, because a
/// repository backed by one file needs "everything in the file", not "one
/// record".
/// </summary>
public interface ICollectionDataReader<T>
{
	IReadOnlyList<T> Read(Stream stream);

	Task<IReadOnlyList<T>> ReadAsync(Stream stream, CancellationToken cancellationToken = default);
}
