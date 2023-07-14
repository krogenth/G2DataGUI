using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Skills;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class SkillCollectionTests
{
    private Skills _skills;
    private static readonly string _testBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.SkillsFile}";
    private static readonly string _skillsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.SkillsPath}";

    [OneTimeSetUp]
    public void SetUp()
    {
		_skills = Skills.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(_skillsFile, _testBackupFile, true);
    }

	[Test]
	public void VerifyAllSKillsRead() =>
		Assert.That(_skills.GameSkills.Count, Is.EqualTo(Skills.NumberOfSkills));

	[Test]
	public void WritesAllSkills() => _skills.Save();

	[Test]
    public void SkillDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(_skillsFile, _testBackupFile);
        Assert.That(result, Is.True);
    }

	[OneTimeTearDown]
	public void TearDown() => File.Copy(_testBackupFile, _skillsFile, true);
}
