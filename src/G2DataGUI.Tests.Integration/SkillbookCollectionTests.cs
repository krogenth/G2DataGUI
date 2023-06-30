using G2DataGUI.Common;
using G2DataGUI.Common.Data.Skills;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class SkillbookCollectionTests
{
    Skillbooks books;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GameFilePaths.SkillbooksFile}";
    static string SkillbooksFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GameFilePaths.SkillbooksPath}";

    [SetUp]
    public void Setup()
    {
        books = Skillbooks.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(SkillbooksFile, TestBackupFile, true);
    }

    [Test]
    public void VerifyAllSkillbooksRead()
    {
        Assert.That(books.GetSkillbooks().Count, Is.EqualTo(Skillbooks.NumberOfSkillbooks));
    }

    [Test]
    public void WritesAllSkillbooks()
    {
        books.Save();
    }

    [Test]
    public void SkillbookDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(SkillbooksFile, TestBackupFile);
        Assert.That(result, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        File.Copy(TestBackupFile, SkillbooksFile, true);
    }
}