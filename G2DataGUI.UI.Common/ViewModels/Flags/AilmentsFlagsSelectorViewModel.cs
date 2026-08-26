using G2DataGUI.Domain.Common;

namespace G2DataGUI.UI.Common.ViewModels.Flags;

public sealed class AilmentsFlagsSelectorViewModel(string title, AilmentTypes initialValue)
	: BaseFlagsSelectorViewModel<AilmentTypes>(title, initialValue);
