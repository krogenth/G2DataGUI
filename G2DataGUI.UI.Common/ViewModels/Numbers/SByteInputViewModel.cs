namespace G2DataGUI.UI.Common.ViewModels.Numbers;

public sealed class SByteInputViewModel(
	string title,
	sbyte initialValue,
	sbyte minValue = sbyte.MinValue,
	sbyte maxValue = sbyte.MaxValue,
	double increment = 1.0d
) : BaseNumberInputViewModel<sbyte>(title, initialValue, minValue, maxValue, increment);
