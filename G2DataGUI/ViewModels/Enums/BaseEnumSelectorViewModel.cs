using System;
using System.Collections.Generic;
using System.Text;
using G2DataGUI.Common.Extensions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Enums;

public abstract class BaseEnumSelectorViewModel<T> : BaseViewModel
	where T : struct, Enum
{
	public event EventHandler<EnumChangedEventArgs>? EnumChanged;

	public IList<T> Items { get; set; }
	public string Title { get; }

	public BaseEnumSelectorViewModel(string title, T initialValue)
	{
		Items = EnumExtensions.GetEnumList<T>();
		Title = title;

		SelectedValueIndex = Items.IndexOf(initialValue);
	}

	public int SelectedValueIndex
	{
		get;
		set
		{
			if (Items.Count == 0)
			{
				return;
			}

			if (value >= Items.Count)
			{
				value = Items.Count - 1;
			}

			if (value < 0)
			{
				value = 0;
			}

			field = value;
			OnPropertyChanged(nameof(SelectedValueIndex));
			EnumChanged?.Invoke(this, new EnumChangedEventArgs(Items[value]));
		}
	}

	public T SelectedValue
	{
		get
		{
			if (SelectedValueIndex < Items.Count && SelectedValueIndex >= 0)
			{
				return Items[SelectedValueIndex];
			}

			return default;
		}
	}

	public class EnumChangedEventArgs(T value) : EventArgs
	{
		public T Value { get; set; } = value;
	}
}
