using System;

namespace G2DataGUI.Common;
public class DifficultyMode
{
	public static DifficultyMode Instance { get; } = new();
	public event EventHandler DifficultyChanged;
	private bool _isHardMode = false;
	public bool IsHardMode
	{
		get => _isHardMode;
		set
		{
			_isHardMode = value;
			DifficultyChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
