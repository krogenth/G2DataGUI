using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace G2DataGUI.UI.Common.ViewModels;

/// <summary>
/// Projects a repository's ObservableCollection&lt;TModel&gt; (Items) into
/// an ObservableCollection&lt;TViewModel&gt; for binding, and keeps the two
/// in sync as the underlying collection changes (LoadAsync clears+refills
/// it, so Reset is the common case — Add/Remove are handled too in case
/// something adds a single item later, e.g. an "Add new Move" feature).
/// One instance per feature — see MovesViewModel for the template; every
/// other repository follows the same one-liner shape.
/// </summary>
public class CollectionViewModel<TModel, TViewModel> : BaseViewModel
	where TModel : class
	where TViewModel : ItemViewModel<TModel>
{
	private readonly ObservableCollection<TModel> _models;
	private readonly Func<TModel, TViewModel> _createViewModel;
	private readonly Dictionary<TModel, TViewModel> _viewModelsByModel = [];

	public CollectionViewModel(ObservableCollection<TModel> models, Func<TModel, TViewModel> createViewModel)
	{
		_models = models;
		_createViewModel = createViewModel;

		Items = [];

		foreach (TModel model in _models)
		{
			Add(model);
		}

		_models.CollectionChanged += OnModelsChanged;
	}

	public ObservableCollection<TViewModel> Items { get; }

	private void OnModelsChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add when e.NewItems is not null:
				foreach (TModel model in e.NewItems)
				{
					Add(model);
				}

				break;

			case NotifyCollectionChangedAction.Remove when e.OldItems is not null:
				foreach (TModel model in e.OldItems)
				{
					Remove(model);
				}

				break;

			case NotifyCollectionChangedAction.Reset:
				Items.Clear();
				_viewModelsByModel.Clear();

				foreach (TModel model in _models)
				{
					Add(model);
				}

				break;
		}
	}

	private void Add(TModel model)
	{
		TViewModel viewModel = _createViewModel(model);

		_viewModelsByModel[model] = viewModel;
		Items.Add(viewModel);
	}

	private void Remove(TModel model)
	{
		if (_viewModelsByModel.Remove(model, out TViewModel? viewModel))
		{
			Items.Remove(viewModel);
		}
	}
}
