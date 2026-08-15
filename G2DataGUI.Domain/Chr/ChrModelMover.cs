namespace G2DataGUI.Domain.Chr;

/// <summary>
/// Moves a ChrModel (and every motion set/animation that belongs to it)
/// from one ChrArchive to another — or reorders it within the same one —
/// then renumbers Index/ModelIndex for every model in both archives
/// afterward so they stay 0-based and sequential.
///
/// Model-to-motion-set ownership is tracked by ChrModel OBJECT REFERENCE
/// throughout, captured once at the start (via GroupByOwner) while it's
/// still unambiguous, and never re-derived from the integer ModelIndex
/// after models from two different archives might sit in the same list —
/// two archives' models will very often share the same original Index
/// value (both archives have "model 0", "model 3", ...), so any attempt
/// to re-match motion sets to models by integer index AFTER merging two
/// archives' lists together risks silently pairing a motion set with the
/// wrong same-numbered model. Reference identity has no such collision.
///
/// This only rearranges Domain objects in memory — nothing here touches a
/// file. Call ChrArchiveRepository.SaveAsync (or write both repositories,
/// for a cross-archive move) afterward to persist it. See
/// ChrArchiveWriter for why moving chunks between archives is safe without
/// understanding every internal pointer.
/// </summary>
public static class ChrModelMover
{
	/// <summary>Moves a single model (with its motions) from
	/// <paramref name="source"/> to <paramref name="destination"/> —
	/// which may be the same archive, to reorder a model within it.
	/// <paramref name="destinationIndex"/> is clamped to a valid insertion
	/// position; omit it to append at the end.</summary>
	public static void Move(
		ChrArchive source,
		int sourceModelIndex,
		ChrArchive destination,
		int? destinationIndex = null)
	{
		ChrModel? model = source.FindModel(sourceModelIndex);

		if (model is null)
		{
			throw new ArgumentOutOfRangeException(
				nameof(sourceModelIndex),
				sourceModelIndex,
				$"No model with index {sourceModelIndex} in the source archive.");
		}

		Dictionary<ChrModel, List<MotionSet>> sourceOwnership = GroupByOwner(source);
		List<MotionSet> movingMotionSets = sourceOwnership[model];

		source.Models.Remove(model);
		sourceOwnership.Remove(model);

		foreach (MotionSet set in movingMotionSets)
		{
			source.MotionSets.Remove(set);
		}

		Renumber(source, sourceOwnership);

		bool sameArchive = ReferenceEquals(source, destination);

		Dictionary<ChrModel, List<MotionSet>> destinationOwnership =
			sameArchive ? sourceOwnership : GroupByOwner(destination);

		int insertAt = Math.Clamp(
			destinationIndex ?? destination.Models.Count,
			0,
			destination.Models.Count);

		destination.Models.Insert(insertAt, model);
		destination.MotionSets.AddRange(movingMotionSets);
		destinationOwnership[model] = movingMotionSets;

		Renumber(destination, destinationOwnership);
	}

	/// <summary>Removes a model (and its motions) from an archive
	/// entirely, without moving it anywhere.</summary>
	public static void Remove(ChrArchive archive, int modelIndex)
	{
		ChrModel? model = archive.FindModel(modelIndex);

		if (model is null)
		{
			return;
		}

		Dictionary<ChrModel, List<MotionSet>> ownership = GroupByOwner(archive);
		List<MotionSet> removedMotionSets = ownership[model];

		archive.Models.Remove(model);
		ownership.Remove(model);

		foreach (MotionSet set in removedMotionSets)
		{
			archive.MotionSets.Remove(set);
		}

		Renumber(archive, ownership);
	}

	/// <summary>Builds the model→motion-sets ownership map from an
	/// archive's CURRENT, still-unambiguous state (every model in a single
	/// archive has a distinct Index, so matching by integer here is safe —
	/// it's only unsafe after two archives' lists have been merged, which
	/// is exactly why this is captured before that happens, not after).</summary>
	private static Dictionary<ChrModel, List<MotionSet>> GroupByOwner(ChrArchive archive)
	{
		Dictionary<ChrModel, List<MotionSet>> ownership = [];

		foreach (ChrModel model in archive.Models)
		{
			ownership[model] = [];
		}

		foreach (MotionSet set in archive.MotionSets)
		{
			ChrModel? owner = archive.Models.FirstOrDefault(m => m.Index == set.ModelIndex);

			if (owner is not null)
			{
				ownership[owner].Add(set);
			}
		}

		return ownership;
	}

	/// <summary>Reassigns every model's Index (and every one of its
	/// motion sets'/animations' ModelIndex) to match current list order,
	/// using the already-known (reference-keyed) ownership map — never
	/// re-derives ownership from ModelIndex here, since by the time this
	/// runs on a destination archive that map may contain models from two
	/// different original archives with overlapping old index values.</summary>
	private static void Renumber(ChrArchive archive, Dictionary<ChrModel, List<MotionSet>> ownership)
	{
		for (int i = 0; i < archive.Models.Count; i++)
		{
			ChrModel model = archive.Models[i];

			model.Index = i;

			List<MotionSet> sets = ownership.TryGetValue(model, out List<MotionSet>? found) ? found : [];

			foreach (MotionSet set in sets)
			{
				set.ModelIndex = i;

				foreach (MotionAnimation animation in set.Animations)
				{
					animation.ModelIndex = i;
				}
			}

			model.Animations = sets.SelectMany(set => set.Animations).ToList();
		}
	}
}
