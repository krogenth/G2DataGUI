namespace G2DataGUI.UI.Common.ViewModels.Numbers;

public sealed class FloatInputViewModel(
	string title,
	float initialValue,
	float minValue = float.MinValue,
	float maxValue = float.MaxValue,
	double increment = 0.1d
) : BaseNumberInputViewModel<float>(title, initialValue, minValue, maxValue, increment);
