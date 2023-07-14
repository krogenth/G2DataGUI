using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Skills;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class SkillbookCollectionTests
{
    private Skillbooks _books;
    private static readonly string _testBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.SkillbooksFile}";
    private static readonly string _skillbooksFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.SkillbooksPath}";

    [OneTimeSetUp]
    public void SetUp()
    {
		_books = Skillbooks.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(_skillbooksFile, _testBackupFile, true);
    }

	[Test]
	public void VerifyAllSkillbooksRead() =>
		Assert.That(_books.GameSkillbooks.Count, Is.EqualTo(Skillbooks.NumberOfSkillbooks));

	[Test]
	public void WritesAllSkillbooks() => _books.Save();

	[Test]
    public void SkillbookDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(_skillbooksFile, _testBackupFile);
        Assert.That(result, Is.True);
    }

	[OneTimeTearDown]
	public void TearDown() => File.Copy(_testBackupFile, _skillbooksFile, true);
}
