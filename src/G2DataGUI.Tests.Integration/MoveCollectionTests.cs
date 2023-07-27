using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class MoveCollectionTests
{
    private Moves _moves;
    private static readonly string _testBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.MovesFile}";
    private static readonly string _movesFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.MovesPath}";

    [OneTimeSetUp]
    public void SetUp()
    {
		_moves = Moves.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(_movesFile, _testBackupFile, true);
    }

	[Test]
	public void VerifyAllMovesRead() =>
		Assert.That(_moves.GameMoves.Count, Is.EqualTo(Moves.NumberOfMoves));

	[Test]
	public void WritesAllMoves() => _moves.Save();

	[Test]
    public void MoveDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(_movesFile, _testBackupFile);
        Assert.That(result, Is.True);
    }

	[OneTimeTearDown]
	public void TearDown() => File.Copy(_testBackupFile, _movesFile, true);
}
