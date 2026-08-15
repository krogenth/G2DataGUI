namespace G2DataGUI.UI.Common.ViewModels.Numbers;

public sealed class ShortInputViewModel(
	string title,
	short initialValue,
	short minValue = short.MinValue,
	short maxValue = short.MaxValue,
	double increment = 1.0d
) : BaseNumberInputViewModel<short>(title, initialValue, minValue, maxValue, increment);
