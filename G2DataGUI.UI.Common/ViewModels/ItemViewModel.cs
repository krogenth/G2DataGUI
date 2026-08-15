using System.Runtime.CompilerServices;
using G2DataGUI.Repositories.Abstractions;

namespace G2DataGUI.UI.Common.ViewModels;

/// <summary>
/// Wraps one domain model item for binding. Domain models deliberately
/// don't implement INotifyPropertyChanged themselves (kept as plain data —
/// see PORTING.md), so this is where that lives: SetModelValue writes the
/// new value onto the model, tells the owning repository the item changed
/// (IRepository.MarkDirty — Save/Load already handle IsDirty on their own,
/// this is the other case), and raises PropertyChanged for the bound
/// control. Feature ViewModels (MoveViewModel, etc.) derive from this and
/// expose one property per model field, each a two-line get/set pair
/// around SetModelValue — see MoveViewModel for the template.
/// </summary>
public abstract class ItemViewModel<TModel>(TModel model, IRepository repository) : BaseViewModel
	where TModel : class
{
	protected TModel Model { get; } = model;

	protected IRepository Repository { get; } = repository;

	protected void SetModelValue<TValue>(
		TValue value,
		Func<TModel, TValue> getter,
		Action<TModel, TValue> setter,
		[CallerMemberName] string propertyName = "")
	{
		if (EqualityComparer<TValue>.Default.Equals(getter(Model), value))
		{
			return;
		}

		setter(Model, value);
		Repository.MarkDirty();
		OnPropertyChanged(propertyName);
	}
}
