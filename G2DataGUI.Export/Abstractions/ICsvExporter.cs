namespace G2DataGUI.Export.Abstractions;

public interface ICsvExporter<T>
{
	string Header { get; }

	void WriteRow(TextWriter writer, T item);
}
