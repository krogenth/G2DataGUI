using System.Collections.ObjectModel;
using G2DataGUI.Domain.Abstractions;
using G2DataGUI.Repositories.Abstractions;

namespace G2DataGUI.Repositories;

/// <summary>
/// For features that don't live in one collection file — instead one
/// *_0.dat per encounter, optionally holding a second entry — see Enemy/
/// Boss. Doesn't derive from Repository&lt;T,TId&gt; because that base
/// assumes exactly one FilePath and one ICollectionDataReader/Writer for
/// the whole set; here every item can have a different backing file and
/// writes go back into that specific file, not a shared one.
///
/// Subclasses supply how to read/write one file's worth of entries and
/// where to find the files; this handles the scan/aggregate/id-assignment
/// loop once for both Enemy and Boss instead of duplicating it.
/// </summary>
public abstract class DirectoryScanRepository<T> : IRepository<T, int>
	where T : class, IIdentifiable<int>
{
	private readonly string _directory;
	private readonly string _searchPattern;

	protected DirectoryScanRepository(string name, string directory, string searchPattern = "*_0.dat")
	{
		Name = name;
		_directory = directory;
		_searchPattern = searchPattern;
	}

	public string Name { get; }

	public ObservableCollection<T> Items { get; } = [];

	IReadOnlyList<T> IRepository<T, int>.Items => Items;

	public bool IsLoaded { get; private set; }

	public bool IsDirty { get; private set; }

	public event EventHandler? StateChanged;

	public T? Find(int id) =>
		Items.FirstOrDefault(x => x.Id == id);

	public bool Contains(int id) =>
		Find(id) is not null;

	/// <summary>Read every entry (one or two) out of a single file, already
	/// positioned at the start. Return them in on-disk order.</summary>
	protected abstract IEnumerable<T> ReadFile(Stream stream, string filePath);

	/// <summary>Write a single entry back to its own FilePath.</summary>
	protected abstract void WriteEntry(T item);

	public async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		List<T> loaded = await Task.Run(() =>
		{
			List<T> result = [];

			if (!Directory.Exists(_directory))
			{
				return result;
			}

			foreach (string file in Directory.GetFiles(_directory, _searchPattern, SearchOption.AllDirectories))
			{
				using FileStream fileStream = File.OpenRead(file);
				using MemoryStream buffer = new();

				fileStream.CopyTo(buffer);
				buffer.Seek(0, SeekOrigin.Begin);

				result.AddRange(ReadFile(buffer, file));
			}

			return result;
		}, cancellationToken).ConfigureAwait(false);

		for (int i = 0; i < loaded.Count; i++)
		{
			loaded[i].Id = i;
		}

		Items.Clear();

		foreach (T item in loaded)
		{
			Items.Add(item);
		}

		IsLoaded = true;
		IsDirty = false;

		OnStateChanged();
	}

	public async Task ReloadAsync(CancellationToken cancellationToken = default) =>
		await LoadAsync(cancellationToken).ConfigureAwait(false);

	public async Task SaveAsync(CancellationToken cancellationToken = default)
	{
		await Task.Run(() =>
		{
			foreach (T item in Items)
			{
				WriteEntry(item);
			}
		}, cancellationToken).ConfigureAwait(false);

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
