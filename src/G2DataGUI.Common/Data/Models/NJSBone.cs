using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Models;

public class NJSBone
{
	public uint Flags { get; set; }
	public NJSMesh Model { get; set; } = null;
	public Vector3 Position { get; set; }
	public Vector3 Angle { get; set; }
	public Vector3 Scale { get; set; }
	public NJSBone Child { get; set; } = null;
	public NJSBone Sibling { get; set; } = null;

	public static NJSBone ReadNJSObject(Stream reader, long instanceOffset)
	{
		NJSBone obj = new()
		{
			Flags = reader.ReadRawUInt(),
		};

		var modelOffset = reader.ReadRawUInt();
		if (modelOffset > 0)
		{
			long position = reader.Position;
			reader.Seek(instanceOffset + modelOffset, SeekOrigin.Begin);
			obj.Model = NJSMesh.ReadNJSModel(reader, instanceOffset);
			reader.Seek(position, SeekOrigin.Begin);
		}

		obj.Position = Vector3.ReadVector3(reader);
		obj.Angle = Vector3.ReadVector3(reader);
		obj.Scale = Vector3.ReadVector3(reader);

		var childOffset = reader.ReadRawUInt();
		if (childOffset > 0)
		{
			long position = reader.Position;
			reader.Seek(instanceOffset + childOffset, SeekOrigin.Begin);
			obj.Child = ReadNJSObject(reader, instanceOffset);
			reader.Seek(position, SeekOrigin.Begin);
		}

		var siblingOffset = reader.ReadRawUInt();
		if (siblingOffset > 0)
		{
			long position = reader.Position;
			reader.Seek(instanceOffset + siblingOffset, SeekOrigin.Begin);
			obj.Sibling = ReadNJSObject(reader, instanceOffset);
			reader.Seek(position, SeekOrigin.Begin);
		}

		return obj;
	}
}
