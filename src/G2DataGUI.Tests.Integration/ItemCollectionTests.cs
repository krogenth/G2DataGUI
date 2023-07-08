using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class ItemCollectionTests
{
    Items items;
    static string TestBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.ItemsFile}";
    static string ItemsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.ItemsPath}";

    [SetUp]
    public void Setup()
    {
        items = Items.Instance;
        Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(ItemsFile, TestBackupFile, true);
    }

    [Test]
    public void VerifyAllItemsRead()
    {
        Assert.That(items.GameItems.Count, Is.EqualTo(Items.NumberOfItems));
    }

    [Test]
    public void WritesAllItems()
    {
        items.Save();
    }

    [Test]
    [Ignore("Data is scrambled, files will not match currently")]
    public void ItemDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(ItemsFile, TestBackupFile);
        Assert.That(result, Is.True);
    }

    [TearDown]
    [Ignore("Data scrambled, do not overwrite original")]
    public void TearDown()
    {
        File.Copy(TestBackupFile, ItemsFile, true);
    }
}