using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class ManaeggCollectionTests
{
    private Manaeggs _eggs;
    private static readonly string _testBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.ManaeggsFile}";
    private static readonly string _manaeggsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.ManaeggsPath}";

    [OneTimeSetUp]
    public void SetUp()
    {
		_eggs = Manaeggs.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(_manaeggsFile, _testBackupFile, true);
    }

	[Test]
	public void VerifyAllManaeggsRead() =>
		Assert.That(_eggs.GameManaeggs.Count, Is.EqualTo(Manaeggs.NumberOfManaeggs));

	[Test]
	public void WritesAllManaeggs() => _eggs.Save();

	[Test]
    public void ManaeggDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(_manaeggsFile, _testBackupFile);
        Assert.That(result, Is.True);
    }

	[OneTimeTearDown]
	public void TearDown() => File.Copy(_testBackupFile, _manaeggsFile, true);
}
