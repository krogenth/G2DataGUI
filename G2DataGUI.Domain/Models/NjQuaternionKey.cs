using System.Numerics;

namespace G2DataGUI.Domain.Models;

public sealed class NjQuaternionKey
{
	public int Frame { get; set; }
	public Quaternion Value { get; set; } = Quaternion.Identity;
}
