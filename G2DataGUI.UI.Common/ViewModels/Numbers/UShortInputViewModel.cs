namespace G2DataGUI.UI.Common.ViewModels.Numbers;

public sealed class UShortInputViewModel(
	string title,
	ushort initialValue,
	ushort minValue = ushort.MinValue,
	ushort maxValue = ushort.MaxValue,
	double increment = 1.0d
) : BaseNumberInputViewModel<ushort>(title, initialValue, minValue, maxValue, increment);
