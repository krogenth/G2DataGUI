namespace G2DataGUI.UI.Common.ViewModels.Numbers;

public sealed class DoubleInputViewModel(
	string title,
	double initialValue,
	double minValue = double.MinValue,
	double maxValue = double.MaxValue,
	double increment = 0.1d
) : BaseNumberInputViewModel<double>(title, initialValue, minValue, maxValue, increment);
