using System.Collections.ObjectModel;
using G2DataGUI.Domain.Abstractions;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Repositories;

/// <summary>
/// Generic file-backed repository. TId is usually int (position-based) for
/// formats with no real on-disk key — see MoveRepository etc. Every feature
/// repository should derive from this rather than hand-rolling IRepository.
/// </summary>
public abstract class Repository<T, TId>(
	string name,
	string filePath,
	ICollectionDataReader<T> reader,
	ICollectionDataWriter<T> writer
) : IRepository<T, TId>
	where T : class, IIdentifiable<TId>
{
	public string Name { get; } = name;

	public string FilePath { get; } = filePath;

	public ObservableCollection<T> Items { get; } = [];

	IReadOnlyList<T> IRepository<T, TId>.Items => Items;

	public bool IsLoaded { get; private set; }

	public bool IsDirty { get; private set; }

	public event EventHandler? StateChanged;

	public T? Find(TId id) =>
		Items.FirstOrDefault(x => EqualityComparer<TId>.Default.Equals(x.Id, id));

	public bool Contains(TId id) =>
		Find(id) is not null;

	public async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		using FileStream stream = File.OpenRead(FilePath);

		IReadOnlyList<T> data =
			await reader.ReadAsync(stream, cancellationToken).ConfigureAwait(true);

		Items.Clear();

		foreach (T item in data)
		{
			Items.Add(item);
		}

		IsLoaded = true;
		IsDirty = false;

		OnStateChanged();
	}

	public async Task ReloadAsync(CancellationToken cancellationToken = default) =>
		await LoadAsync(cancellationToken).ConfigureAwait(true);

	public async Task SaveAsync(CancellationToken cancellationToken = default)
	{
		using FileStream stream = File.Create(FilePath);

		await writer.WriteAsync(stream, Items, cancellationToken).ConfigureAwait(true);

		IsDirty = false;

		OnStateChanged();
	}

	public void MarkDirty()
	{
		if (!IsDirty)
		{
			IsDirty = true;
			OnStateChanged();
		}
	}

	protected virtual void OnStateChanged() =>
		StateChanged?.Invoke(this, EventArgs.Empty);
}
