using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Models;

public class NJSMesh
{
	public NJSVertexList VertexList { get; set; } = null;
	// indices
	public Vector3 Center { get; set; }
	public float Radius { get; set; }

	public static NJSMesh ReadNJSModel(Stream reader, long instanceOffset)
	{
		NJSMesh model = new();

		var vertexOffset = reader.ReadRawUInt();
		if (vertexOffset > 0)
		{
			var position = reader.Position;
			reader.Seek(instanceOffset + vertexOffset, SeekOrigin.Begin);
			model.VertexList = NJSVertexList.ReadNJSVertexList(reader);
			reader.Seek(position, SeekOrigin.Begin);
		}

		model.Center = Vector3.ReadVector3(reader);
		model.Radius = reader.ReadRawFloat();

		return model;
	}
}
