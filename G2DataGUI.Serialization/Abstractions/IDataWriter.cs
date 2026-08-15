namespace G2DataGUI.Serialization.Abstractions;

/// <summary>Writes exactly one T at the stream's current location. See IDataReader.</summary>
public interface IDataWriter<T>
{
	void Write(Stream stream, T item);

	Task WriteAsync(Stream stream, T item, CancellationToken cancellationToken = default);
}
