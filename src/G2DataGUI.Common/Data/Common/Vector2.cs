using System.IO;

using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Common;

public class Vector2
{
    public float X { get; set; }
    public float Z { get; set; }

	public static Vector2 ReadVector2(Stream reader)
    {
        Vector2 vector2 = new()
        {
            X = reader.ReadRawFloat(),
            Z = reader.ReadRawFloat(),
        };
        return vector2;
    }

    public void WriteVector2(Stream writer)
    {
        writer.WriteRawFloat(X);
        writer.WriteRawFloat(Z);
    }
}
