using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Skills;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class SkillCollectionTests
{
    Skills skills;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.SkillsFile}";
    static string SkillsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.SkillsPath}";

    [SetUp]
    public void Setup()
    {
        skills = Skills.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(SkillsFile, TestBackupFile, true);
    }

    [Test]
    public void VerifyAllSKillsRead()
    {
        Assert.That(skills.GameSkills.Count, Is.EqualTo(Skills.NumberOfSkills));
    }

    [Test]
    public void WritesAllSkills()
    {
        skills.Save();
    }

    [Test]
    public void SkillDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(SkillsFile, TestBackupFile);
        Assert.That(result, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        File.Copy(TestBackupFile, SkillsFile, true);
    }
}