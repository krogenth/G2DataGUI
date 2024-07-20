using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Common;

public class Vector3
{
    public float X { get; set; }
    public float Y { get; set; }
	public float Z { get; set; }

	public static Vector3 operator+(Vector3 left, Vector3 right)
	{
		return new Vector3
		{
			X = left.X + right.X,
			Y = left.Y + right.Y,
			Z = left.Z + right.Z,
		};
	}

	public static Vector3 operator-(Vector3 left, Vector3 right)
	{
		return new Vector3
		{
			X = left.X - right.X,
			Y = left.Y - right.Y,
			Z = left.Z - right.Z,
		};
	}

	public static Vector3 ReadVector3(Stream reader)
    {
        Vector3 vector3 = new()
        {
            X = reader.ReadRawFloat(),
            Y = reader.ReadRawFloat(),
            Z = reader.ReadRawFloat(),
        };
        return vector3;
    }

    public void WriteVector3(Stream writer)
    {
        writer.WriteRawFloat(X);
        writer.WriteRawFloat(Y);
        writer.WriteRawFloat(Z);
    }
}
