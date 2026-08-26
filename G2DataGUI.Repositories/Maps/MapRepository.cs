using System.Collections.ObjectModel;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.Serialization.Maps;

namespace G2DataGUI.Repositories.Maps;

/// <summary>
/// Discovers map folders under map/ — one subfolder per map, matched by
/// its own name having a [name].mdt inside (map/a410/a410.mdt). Each
/// found .mdt is fully parsed via MapReader eagerly, at LoadAsync time —
/// see MapReader's own doc comment on what "fully parsed" does and
/// doesn't cover yet, particularly the dialogue section.
///
/// This is deliberately the eager version, not lazy-per-map-selection —
/// a reported 35GB+ memory spike turned out to be an actual infinite loop
/// in MapDialogueSectionReader (fixed there, independently of this file),
/// not simply the cost of eager loading. Testing eager loading with just
/// that fix in place, before adding the complexity of lazy per-map
/// loading, to see whether the simpler fix alone is sufficient.
///
/// map/a410/a410.chr or .chr2 is picked up alongside if present, and so
/// is map/a410/textures_a410.pvl/ (the DDS texture folder — note the
/// asymmetric naming: textures_[name], not [name]_textures or [name]/textures).
///
/// SaveAsync writes every map's Header/Entries/Instances/HTAs/Scripts/
/// EnemyPositions/EnemyGroups/MOSs/Icons/Shop back via MapWriter — NOT the
/// Dialogue section (see MapDialogueReader's own doc comment on why that's
/// read-only). IsDirty is per-repository (any map dirty marks the whole
/// repository dirty), same granularity as every other DirectoryScanRepository-
/// style repository in this project.
/// </summary>
public sealed class MapRepository(string mapDirectory) : IRepository<Map, int>
{
	private const string TexturesFolderPrefix = "textures_";

	public string Name => "Maps";

	public ObservableCollection<Map> Items { get; } = [];

	IReadOnlyList<Map> IRepository<Map, int>.Items => Items;

	public bool IsLoaded { get; private set; }

	public bool IsDirty { get; private set; }

	public event EventHandler? StateChanged;

	public Map? Find(int id) =>
		Items.FirstOrDefault(m => m.Id == id);

	public bool Contains(int id) =>
		Find(id) is not null;

	public async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		List<Map> discovered = await Task.Run(() =>
		{
			List<Map> result = [];

			if (!Directory.Exists(mapDirectory))
			{
				return result;
			}

			foreach (string folder in Directory.GetDirectories(mapDirectory))
			{
				string name = Path.GetFileName(folder);
				string mdtPath = Path.Combine(folder, name + ".mdt");

				if (!File.Exists(mdtPath))
				{
					continue;
				}

				string chrPath = Path.Combine(folder, name + ".chr");
				string chr2Path = Path.Combine(folder, name + ".chr2");

				string? resolvedChrPath =
					File.Exists(chrPath) ? chrPath :
					File.Exists(chr2Path) ? chr2Path :
					null;

				string texturesFolder = Path.Combine(folder, TexturesFolderPrefix + name + ".pvl");
				string? resolvedTexturesPath = Directory.Exists(texturesFolder) ? texturesFolder : null;

				using FileStream stream = File.OpenRead(mdtPath);

				result.Add(MapReader.Read(stream, name, mdtPath, resolvedChrPath, resolvedTexturesPath));
			}

			return result;
		}, cancellationToken).ConfigureAwait(true);

		discovered.Sort((a, b) => string.CompareOrdinal(a.Name, b.Name));

		for (int i = 0; i < discovered.Count; i++)
		{
			discovered[i].Id = i;
		}

		Items.Clear();

		foreach (Map map in discovered)
		{
			Items.Add(map);
		}

		IsLoaded = true;
		IsDirty = false;

		OnStateChanged();
	}

	public async Task ReloadAsync(CancellationToken cancellationToken = default) =>
		await LoadAsync(cancellationToken).ConfigureAwait(true);

	public async Task SaveAsync(CancellationToken cancellationToken = default)
	{
		await Task.Run(() =>
		{
			foreach (Map map in Items)
			{
				using FileStream stream = File.Open(map.MdtPath, FileMode.Open, FileAccess.ReadWrite);

				MapWriter.Write(stream, map);
			}
		}, cancellationToken).ConfigureAwait(true);

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
