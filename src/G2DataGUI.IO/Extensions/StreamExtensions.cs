using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.IO.Streams;

public static class StreamExtensions
{
    public static T? ReadStruct<T>(this Stream stream) where T : struct
    {
        if (stream == null) return null;

        int size = Marshal.SizeOf(typeof(T));
        byte[] bytes = new byte[size];
        if (stream.Read(bytes, 0, size) != size)
        {
            // can't build this structure!
            return null;
        }

        GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        try
        {
            return (T?)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        }
        finally
        {
            handle.Free();
        }
    }

    public static void WriteStruct<T>(this Stream stream, T obj) where T : struct
    {
        if (stream == null) return;
        int length = Marshal.SizeOf(obj);
        byte[] objByteArr = new byte[length];
        IntPtr ptr = Marshal.AllocHGlobal(length);
        Marshal.StructureToPtr(obj, ptr, false);
        Marshal.Copy(ptr, objByteArr, 0, length);
        Marshal.FreeHGlobal(ptr);
        stream.Write(objByteArr, 0, length);
    }

    public static byte[] StreamToBytesArray(this Stream stream)
    {
        using (MemoryStream mStream = new MemoryStream())
        {
            stream.CopyTo(mStream);
            return mStream.ToArray();
        }
    }

    public static sbyte ReadRawSByte(this Stream stream) => (sbyte)stream.InternalRead(1)[0];
    public static byte ReadRawByte(this Stream stream) => stream.InternalRead(1)[0];
    public static short ReadRawShort(this Stream stream) => BinaryPrimitives.ReadInt16LittleEndian(stream.InternalRead(2));
    public static ushort ReadRawUShort(this Stream stream) => BinaryPrimitives.ReadUInt16LittleEndian(stream.InternalRead(2));
    public static int ReadRawInt(this Stream stream) => BinaryPrimitives.ReadInt32LittleEndian(stream.InternalRead(4));
    public static uint ReadRawUInt(this Stream stream) => BinaryPrimitives.ReadUInt32LittleEndian(stream.InternalRead(4));
    public static long ReadRawLong(this Stream stream) => BinaryPrimitives.ReadInt64LittleEndian(stream.InternalRead(4));
    public static ulong ReadRawULong(this Stream stream) => BinaryPrimitives.ReadUInt64LittleEndian(stream.InternalRead(4));
    public static Half ReadRawHalf(this Stream stream) => BinaryPrimitives.ReadHalfLittleEndian(stream.InternalRead(2));
    public static float ReadRawFloat(this Stream stream) => BitConverter.Int32BitsToSingle(stream.ReadRawInt());
    public static double ReadRawDouble(this Stream stream) => BitConverter.Int64BitsToDouble(stream.ReadRawInt());

    private static Span<byte> InternalRead(this Stream stream, int numBytes)
    {
        var buffer = new byte[numBytes];
        stream.ReadExactly(buffer.AsSpan(0, numBytes));
        return buffer;
    }

    public static void WriteRawByte(this Stream stream, byte value) => stream.WriteByte(value);
    public static void WriteRawSByte(this Stream stream, sbyte value) => stream.WriteByte((byte)value);
    public static void WriteRawShort(this Stream stream, short value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(short)];
        BinaryPrimitives.WriteInt16LittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawUShort(this Stream stream, ushort value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(ushort)];
        BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawInt(this Stream stream, int value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawUInt(this Stream stream, uint value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(uint)];
        BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawLong(this Stream stream, long value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(long)];
        BinaryPrimitives.WriteInt64LittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawULong(this Stream stream, ulong value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(ulong)];
        BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawHalf(this Stream stream, Half value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(short)];
        BinaryPrimitives.WriteHalfLittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawFloat(this Stream stream, float value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(float)];
        BinaryPrimitives.WriteSingleLittleEndian(buffer, value);
        stream.Write(buffer);
    }
    public static void WriteRawDouble(this Stream stream, double value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(double)];
        BinaryPrimitives.WriteDoubleLittleEndian(buffer, value);
        stream.Write(buffer);
    }
}