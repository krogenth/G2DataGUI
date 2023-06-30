using G2DataGUI.Common;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class MoveCollectionTests
{
    Moves moves;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GameFilePaths.MovesFile}";
    static string MovesFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GameFilePaths.MovesPath}";

    [SetUp]
    public void Setup()
    {
        moves = Moves.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(MovesFile, TestBackupFile, true);
    }

    [Test]
    public void VerifyAllMovesRead()
    {
        Assert.That(moves.GetMoves().Count, Is.EqualTo(Moves.NumberOfMoves));
    }

    [Test]
    public void WritesAllMoves()
    {
        moves.Save();
    }

    [Test]
    public void MoveDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(MovesFile, TestBackupFile);
        Assert.That(result, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        File.Copy(TestBackupFile, MovesFile, true);
    }
}