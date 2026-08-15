namespace G2DataGUI.Serialization.Abstractions;

/// <summary>Writes every record of a collection back to a stream. See ICollectionDataReader.</summary>
public interface ICollectionDataWriter<T>
{
	void Write(Stream stream, IEnumerable<T> items);

	Task WriteAsync(Stream stream, IEnumerable<T> items, CancellationToken cancellationToken = default);
}
