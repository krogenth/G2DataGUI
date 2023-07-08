using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class ManaeggCollectionTests
{
    Manaeggs eggs;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.ManaeggsFile}";
    static string ManaeggsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.ManaeggsPath}";

    [SetUp]
    public void Setup()
    {
        eggs = Manaeggs.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(ManaeggsFile, TestBackupFile, true);
    }

    [Test]
    public void VerifyAllManaeggsRead()
    {
        Assert.That(eggs.GameManaeggs.Count, Is.EqualTo(Manaeggs.NumberOfManaeggs));
    }

    [Test]
    public void WritesAllManaeggs()
    {
        eggs.Save();
    }

    [Test]
    public void ManaeggDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(ManaeggsFile, TestBackupFile);
        Assert.That(result, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        File.Copy(TestBackupFile, ManaeggsFile, true);
    }
}