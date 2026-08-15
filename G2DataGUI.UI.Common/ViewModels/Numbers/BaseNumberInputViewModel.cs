using System.Numerics;

namespace G2DataGUI.UI.Common.ViewModels.Numbers;

/// <summary>
/// Backs a bounded numeric input control (a NumericUpDown,
/// typically) for one of .NET's built-in numeric primitives — every
/// primitive numeric type implements IMinMaxValue<TSelf>, which is
/// what makes MinValue/MaxValue's defaults below able to fall back to the
/// type's own real range without each concrete subclass (ByteInputViewModel,
/// IntInputViewModel, ...) needing to repeat it.
/// </summary>
public abstract class BaseNumberInputViewModel<T>(string title, T initialValue, T minValue, T maxValue, double increment = 1.0d) : BaseViewModel
	where T : IComparable<T>, IEquatable<T>, IMinMaxValue<T>
{
	public event EventHandler<NumberChangedEventArgs>? ValueChanged;

	public string Title { get; } = title;

	public T Value
	{
		get;
		set
		{
			field = value;
			OnPropertyChanged(nameof(Value));
			ValueChanged?.Invoke(this, new NumberChangedEventArgs(value));
		}
	} = initialValue;

	public T MinValue { get; } = minValue;
	public T MaxValue { get; } = maxValue;
	public double Increment { get; } = increment;

	public sealed class NumberChangedEventArgs(T value) : EventArgs
	{
		public T Value { get; } = value;
	}
}
