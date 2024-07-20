using G2DataGUI.Common.Data.Common;

namespace G2DataGUI.Common.Data.Models;
public class NJSVertex
{
	public Vector3 Position { get; set; }
	public Vector3 AbsolutePosition => Position + ParentBone.AbsolutePosition;
	public NJSBone ParentBone { get; set; }
}
