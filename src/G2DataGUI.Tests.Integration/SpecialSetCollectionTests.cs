using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class SpecialSetCollectionTests
{
    SpecialSets specialSets;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.SpecialsFile}";
    static string SpecialSetsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.SpecialsPath}";

    [SetUp]
    public void Setup()
    {
        specialSets = SpecialSets.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(SpecialSetsFile, TestBackupFile, true);
    }

    [Test]
    public void VerifyAllSpecialSetsRead()
    {
        Assert.That(specialSets.GameSpecialSets.Count, Is.EqualTo(SpecialSets.NumberOfSpecialSets));
    }

    [Test]
    public void WritesAllSpecialSets()
    {
        specialSets.Save();
    }

    [Test]
    public void SpecialSetDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(SpecialSetsFile, TestBackupFile);
        Assert.That(result, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        File.Copy(TestBackupFile, SpecialSetsFile, true);
    }
}