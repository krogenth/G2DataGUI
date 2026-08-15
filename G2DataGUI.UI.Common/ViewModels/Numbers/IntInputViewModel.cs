namespace G2DataGUI.UI.Common.ViewModels.Numbers;

public sealed class IntInputViewModel(
	string title,
	int initialValue,
	int minValue = int.MinValue,
	int maxValue = int.MaxValue,
	double increment = 1.0d
) : BaseNumberInputViewModel<int>(title, initialValue, minValue, maxValue, increment);
