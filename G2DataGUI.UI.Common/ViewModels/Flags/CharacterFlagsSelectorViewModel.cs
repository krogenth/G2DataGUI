using G2DataGUI.Domain.Common;

namespace G2DataGUI.UI.Common.ViewModels.Flags;

public sealed class CharacterFlagsSelectorViewModel(string title, CharacterTypes initialValue)
	: BaseFlagsSelectorViewModel<CharacterTypes>(title, initialValue);
