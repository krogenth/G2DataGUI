using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Models;

/// <summary>
/// Defines the njcm_object_t structure from the
/// Dreamcast Katana SDK's Ninja library specifies.
/// </summary>
public class NJSBone
{
	public uint Flags { get; set; }
	public NJSBone Parent { get; set; }
	public NJSMesh Mesh { get; set; } = null;
	public Vector3 Position { get; set; }
	public Vector3 Angle { get; set; }
	public Vector3 Scale { get; set; }
	public NJSBone Child { get; set; } = null;
	public NJSBone Sibling { get; set; } = null;
	public Vector3 AbsolutePosition => Parent is not null ? Position + Parent.AbsolutePosition : Position;

	public static NJSBone ReadNJSObject(Stream reader, long instanceOffset,  NJSBone parent = null)
	{
		NJSBone bone = new()
		{
			Flags = reader.ReadRawUInt(),
			Parent = parent,
		};

		var modelOffset = reader.ReadRawUInt();
		if (modelOffset > 0)
		{
			long position = reader.Position;
			reader.Seek(instanceOffset + modelOffset, SeekOrigin.Begin);
			bone.Mesh = NJSMesh.ReadNJSModel(reader, instanceOffset, bone);
			reader.Seek(position, SeekOrigin.Begin);
		}

		bone.Position = Vector3.ReadVector3(reader);
		bone.Angle = Vector3.ReadVector3(reader);
		bone.Scale = Vector3.ReadVector3(reader);

		var childOffset = reader.ReadRawUInt();
		if (childOffset > 0)
		{
			long position = reader.Position;
			reader.Seek(instanceOffset + childOffset, SeekOrigin.Begin);
			bone.Child = ReadNJSObject(reader, instanceOffset, bone);
			reader.Seek(position, SeekOrigin.Begin);
		}

		var siblingOffset = reader.ReadRawUInt();
		if (siblingOffset > 0)
		{
			long position = reader.Position;
			reader.Seek(instanceOffset + siblingOffset, SeekOrigin.Begin);
			bone.Sibling = ReadNJSObject(reader, instanceOffset, bone);
			reader.Seek(position, SeekOrigin.Begin);
		}

		return bone;
	}
}
