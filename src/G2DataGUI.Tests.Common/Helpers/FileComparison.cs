using System.IO;

namespace G2DataGUI.Tests.Common.Helpers;

public static class FileComparison
{
    public static bool FileCompare(string file1, string file2)
    {
        int file1Byte;
        int file2Byte;
        FileStream stream1;
        FileStream stream2;

        if (file1 == file2)
		{
			return true;
		}

		stream1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
        stream2 = new FileStream(file2, FileMode.Open, FileAccess.Read);

        if (stream1.Length != stream2.Length)
        {
            stream1.Close();
            stream2.Close();
            return false;
        }

        do
        {
            file1Byte = stream1.ReadByte();
            file2Byte = stream2.ReadByte();
        } while (file1Byte == file2Byte && file1Byte != -1);

        stream1.Close();
        stream2.Close();
        return file1Byte == file2Byte;
    }
}
