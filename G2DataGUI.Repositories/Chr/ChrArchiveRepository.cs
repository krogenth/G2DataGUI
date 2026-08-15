using G2DataGUI.Domain.Chr;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.Serialization.Chr;

namespace G2DataGUI.Repositories.Chr;

/// <summary>
/// A .chr/.chr2 archive is a single document, not a collection of
/// same-shaped items — there's no natural TId for "an archive", so this
/// implements IRepository directly (Name/IsLoaded/MarkDirty/Load/Save/
/// Reload) rather than IRepository&lt;T,TId&gt;. Archive is the loaded
/// document itself; editing it (removing/inserting/moving a ChrModel) is
/// done directly against Archive.Models and Archive.Chunks, then MarkDirty()
/// — see ChrModelMover for the cross-archive move operation specifically.
/// </summary>
public sealed class ChrArchiveRepository(string filePath) : IRepository
{
	public string Name => "ChrArchive";

	public bool IsLoaded { get; private set; }

	public bool IsDirty { get; private set; }

	public ChrArchive? Archive { get; private set; }

	public event EventHandler? StateChanged;

	public async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		await Task.Run(() =>
		{
			using FileStream stream = File.OpenRead(filePath);

			Archive = new ChrArchiveReader().Read(stream);
			Archive.FileName = filePath;
		}, cancellationToken).ConfigureAwait(false);

		IsLoaded = true;
		IsDirty = false;

		OnStateChanged();
	}

	public async Task ReloadAsync(CancellationToken cancellationToken = default) =>
		await LoadAsync(cancellationToken).ConfigureAwait(false);

	public async Task SaveAsync(CancellationToken cancellationToken = default)
	{
		if (Archive is null)
		{
			return;
		}

		await Task.Run(() =>
		{
			using FileStream stream = File.Create(filePath);

			new ChrArchiveWriter().Write(stream, Archive);
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

	private void OnStateChanged() =>
		StateChanged?.Invoke(this, EventArgs.Empty);
}
