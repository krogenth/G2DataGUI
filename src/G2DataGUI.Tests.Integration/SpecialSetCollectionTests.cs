using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class SpecialSetCollectionTests
{
    private SpecialSets _specialSets;
    private static readonly string _testBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.SpecialsFile}";
    private static readonly string _specialSetsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.SpecialsPath}";

    [OneTimeSetUp]
    public void SetUp()
    {
		_specialSets = SpecialSets.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(_specialSetsFile, _testBackupFile, true);
    }

	[Test]
	public void VerifyAllSpecialSetsRead() =>
		Assert.That(_specialSets.GameSpecialSets.Count, Is.EqualTo(SpecialSets.NumberOfSpecialSets));

	[Test]
	public void WritesAllSpecialSets() => _specialSets.Save();

	[Test]
    public void SpecialSetDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(_specialSetsFile, _testBackupFile);
        Assert.That(result, Is.True);
    }

	[OneTimeTearDown]
	public void TearDown() => File.Copy(_testBackupFile, _specialSetsFile, true);
}
