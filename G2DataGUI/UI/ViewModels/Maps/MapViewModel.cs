using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using G2DataGUI.Domain.Chr;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Models;
using G2DataGUI.Repositories.Maps;
using G2DataGUI.Serialization.Chr;
using G2DataGUI.Serialization.Models;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;
using G2DataGUI.UI.ViewModels.Maps.Sections;

namespace G2DataGUI.UI.ViewModels.Maps;

/// <summary>
/// Eager version — Entries/Instances/HTAs/Scripts/EnemyPositions/
/// EnemyGroups/MOSs/Icons/Dialogue/Shop are all built here, in the
/// constructor, from whatever MapRepository already parsed at LoadAsync
/// time (see MapRepository's own doc comment on why this is the eager
/// version rather than the lazy-per-selection one — testing whether the
/// MapDialogueSectionReader infinite-loop fix alone is enough before
/// adding lazy-loading complexity on top of it).
///
/// LoadPreviewAsync (the 3D model/motion preview) stays lazy regardless —
/// that was always a separate concern from .mdt section parsing, costs
/// real time/memory on its own (NJCM/DDS parsing), and most maps' 3D
/// previews are never opened even when their .mdt data already was.
///
/// SelectModel/SelectMotion do the actual per-model/per-motion NJCM/NMDM
/// parsing — both are pure CPU-bound work (no file I/O once the archive
/// itself is loaded), so unlike LoadPreviewAsync they run synchronously
/// off a ComboBox selection change rather than needing their own
/// Task.Run.
///
/// SelectedMotion only sets PreviewMotion (the parsed NjMotion) — it does
/// NOT drive playback itself. Advancing Frame over time and pushing it
/// into ModelViewport.SetAnimation on a timer is MapsView.axaml.cs's job
/// (a DispatcherTimer), since frame advancement is a UI-thread animation-
/// loop concern, not view-model state.
///
/// Nothing here writes back to the archive — the 3D preview is still
/// read-only, not the archive editor described in PORTING-Chr.md. The
/// .mdt section editing above IS a real write path, though — MapRepository.
/// SaveAsync (via MapWriter) persists every section here except Dialogue.
/// </summary>
public sealed class MapViewModel(
	Map model,
	MapRepository repository,
	ObservableCollection<GameItemViewModel> availableItems,
	ObservableCollection<MapViewModel> availableMaps
) : ItemViewModel<Map>(model, repository)
{
	private ChrArchive? _archive;
	private List<DdsTexture> _texturePool = [];

	public int Id => Model.Id;
	public string Name => Model.Name;
	public string MapName => Model.MapName;
	public string MdtPath => Model.MdtPath;
	public bool HasChrArchive => Model.ChrPath is not null;

	public MapEntriesViewModel Entries { get; } = new MapEntriesViewModel(model.Entries.Select(entry => new MapEntryViewModel(entry, repository, availableMaps)));
	public MapInstancesViewModel Instances { get; } = new MapInstancesViewModel(model.Instances.Select(instance => new MapInstanceViewModel(instance, repository)));
	public MapHTAsViewModel HTAs { get; } = new MapHTAsViewModel(model.HTAs.Select(hta => new MapHTAViewModel(hta, repository)));
	public MapScriptsViewModel Scripts { get; } = new MapScriptsViewModel(model.Scripts.Select(script => new MapScriptViewModel(script, repository, availableMaps)));
	public MapEnemyPositionsViewModel EnemyPositions { get; } = new MapEnemyPositionsViewModel(model.EnemyPositions.Select(position => new MapEnemyPositionViewModel(position, repository)));
	public MapEnemyGroupsViewModel EnemyGroups { get; } = new MapEnemyGroupsViewModel(model.EnemyGroups.Select(group => new MapEnemyGroupViewModel(group, repository)));
	public MapMOSsViewModel MOSs { get; } = new MapMOSsViewModel(model.MOSs.Select(mos => new MapMOSViewModel(mos, repository)));
	public MapIconsViewModel Icons { get; } = new MapIconsViewModel(model.Icons.Select(icon => new MapIconViewModel(icon, repository, availableItems)));
	public MapDialogueEntriesViewModel Dialogue { get; } = new MapDialogueEntriesViewModel(model.Dialogue.Entries.Select(entry => new MapDialogueEntryViewModel(entry, repository)));
	public MapShopViewModel? Shop { get; } = model.Shop is not null ? new MapShopViewModel(model.Shop, repository, availableItems) : null;

	public IReadOnlyList<ChrModel> AvailableModels { get; private set; } = [];

	public ChrModel? SelectedModel { get; private set; }

	public NjModel? PreviewModel { get; private set; }

	public IReadOnlyList<DdsTexture?> PreviewTextures { get; private set; } = [];

	public IReadOnlyList<MotionAnimation> AvailableMotions => SelectedModel?.Animations ?? [];

	public MotionAnimation? SelectedMotion { get; private set; }

	public NjMotion? PreviewMotion { get; private set; }

	public bool IsLoadingPreview { get; private set; }

	public string? PreviewError { get; private set; }

	public async Task LoadPreviewAsync(CancellationToken cancellationToken = default)
	{
		if (Model.ChrPath is null || _archive is not null || IsLoadingPreview)
		{
			return;
		}

		IsLoadingPreview = true;
		PreviewError = null;
		OnPropertyChanged(nameof(IsLoadingPreview));

		try
		{
			(ChrArchive archive, List<DdsTexture> texturePool) =
				await Task.Run(LoadArchive, cancellationToken).ConfigureAwait(true);

			_archive = archive;
			_texturePool = texturePool;

			AvailableModels = archive.Models;
			OnPropertyChanged(nameof(AvailableModels));

			if (archive.Models.Count == 0)
			{
				PreviewError = "Archive has no models.";
			}
			else
			{
				SelectModel(archive.Models[0]);
			}
		}
		catch (Exception ex)
		{
			PreviewError = $"Couldn't load archive: {ex.Message}";
		}
		finally
		{
			IsLoadingPreview = false;
			OnPropertyChanged(nameof(IsLoadingPreview));
			OnPropertyChanged(nameof(PreviewError));
		}
	}

	public void SelectModel(ChrModel chrModel)
	{
		SelectedModel = chrModel;
		PreviewModel = NjModelReader.Read(chrModel.NjcmChunk.Payload);
		PreviewTextures = ChrTextureBinder.BindTextures(chrModel, _texturePool);

		OnPropertyChanged(nameof(SelectedModel));
		OnPropertyChanged(nameof(PreviewModel));
		OnPropertyChanged(nameof(PreviewTextures));
		OnPropertyChanged(nameof(AvailableMotions));

		// Switching models invalidates whatever motion was selected — a
		// motion's bone tracks are indexed against ITS model's own bone
		// list, so keeping SelectedMotion pointed at a motion that
		// belonged to the previous model would evaluate against the
		// wrong skeleton.
		SelectMotion(null);
	}

	public void SelectMotion(MotionAnimation? motion)
	{
		SelectedMotion = motion;
		PreviewMotion = motion is not null && PreviewModel is not null
			? NjMotionReader.Read(motion.RawNmdm, PreviewModel.Bones.Count)
			: null;

		OnPropertyChanged(nameof(SelectedMotion));
		OnPropertyChanged(nameof(PreviewMotion));
	}

	private (ChrArchive Archive, List<DdsTexture> TexturePool) LoadArchive()
	{
		using FileStream stream = File.OpenRead(Model.ChrPath!);

		ChrArchive archive = new ChrArchiveReader().Read(stream);

		List<DdsTexture> texturePool = [];

		if (Model.TexturesPath is not null)
		{
			foreach (string ddsPath in Directory.GetFiles(Model.TexturesPath, "*.dds"))
			{
				texturePool.Add(DdsReader.Read(File.ReadAllBytes(ddsPath), Path.GetFileName(ddsPath)));
			}
		}

		return (archive, texturePool);
	}
}
