using System.Numerics;

namespace G2DataGUI.Domain.Models;

/// <summary>
/// One Euler-angle rotation keyframe. Value is radians, already
/// converted from the file's 32-bit BAMS angle units.
/// </summary>
public sealed class NjEuler32Key
{
	public int Frame { get; set; }
	public Vector3 Value { get; set; }
}
