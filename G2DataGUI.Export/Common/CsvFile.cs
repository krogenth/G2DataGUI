using G2DataGUI.Export.Abstractions;

namespace G2DataGUI.Export.Common;

public static class CsvFile
{
	public static void Write<T>(string path, IEnumerable<T> items, ICsvExporter<T> exporter)
	{
		using StreamWriter writer = new(path);

		writer.WriteLine(exporter.Header);

		foreach (T item in items)
		{
			exporter.WriteRow(writer, item);
		}
	}

	public static async Task WriteAsync<T>(
		string path,
		IEnumerable<T> items,
		ICsvExporter<T> exporter,
		CancellationToken cancellationToken = default)
	{
		using StreamWriter writer = new(path);

		await writer.WriteLineAsync(exporter.Header).ConfigureAwait(true);

		foreach (T item in items)
		{
			cancellationToken.ThrowIfCancellationRequested();
			exporter.WriteRow(writer, item);
		}
	}
}
