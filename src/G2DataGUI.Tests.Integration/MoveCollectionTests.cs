using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class MoveCollectionTests
{
    Moves moves;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.MovesFile}";
    static string MovesFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.MovesPath}";

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
        Assert.That(moves.GameMoves.Count, Is.EqualTo(Moves.NumberOfMoves));
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