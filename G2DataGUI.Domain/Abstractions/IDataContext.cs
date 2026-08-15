namespace G2DataGUI.Domain.Abstractions;

/// <summary>
/// Anything that can be loaded from disk, saved back, reloaded, and report
/// whether it has unsaved changes. Deliberately has no I/O in its signature
/// (just Streams/paths are someone else's problem) so it can live in Domain
/// alongside IIdentifiable — both Repositories (which implement it per
/// feature) and Contexts (which aggregate it) depend on Domain already, so
/// putting it here avoids Contexts and Repositories needing to reference
/// each other for this one shared shape.
/// </summary>
public interface IDataContext
{
	bool IsDirty { get; }

	Task LoadAsync(CancellationToken cancellationToken = default);

	Task SaveAsync(CancellationToken cancellationToken = default);

	Task ReloadAsync(CancellationToken cancellationToken = default);
}
