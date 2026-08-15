using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Repositories.Abstractions;

public interface IRepository : IDataContext
{
	string Name { get; }

	bool IsLoaded { get; }

	/// <summary>
	/// Called by ViewModels when the user edits an item's field —
	/// domain models don't implement INotifyPropertyChanged themselves (see
	/// PORTING.md), so there's no other signal that tells the repository an
	/// in-place edit happened. Load/Save/Reload set IsDirty internally;
	/// this is the one entry point for everything else.
	/// </summary>
	void MarkDirty();
}

public interface IRepository<T, TId> : IRepository
	where T : class
{
	IReadOnlyList<T> Items { get; }

	T? Find(TId id);

	bool Contains(TId id);
}
