using G2DataGUI.Common.Paths;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Tests.Common;
using G2DataGUI.Tests.Common.Helpers;

namespace G2DataGUI.Tests.Integration;

[TestFixture]
[Category("Integration")]
public class ItemCollectionTests
{
	private Items _items;
	private static readonly string _testBackupFile = $"{Constants.TestBackupDirectory}{GamePaths.ItemsFile}";
	private static readonly string _itemsFile = $"{G2DataGUI.Common.Version.Instance.RootDataDirectory}{GamePaths.ItemsPath}";

	[OneTimeSetUp]
    public void SetUp()
    {
		_items = Items.Instance;
		Directory.CreateDirectory(Constants.TestBackupDirectory);
        File.Copy(_itemsFile, _testBackupFile, true);
    }

	[Test]
	public void VerifyAllItemsRead() => Assert.That(_items.GameItems.Count, Is.EqualTo(Items.NumberOfItems));

	[Test]
	public void WritesAllItems() => _items.Save();

	[Test]
    [Ignore("Data is scrambled, files will not match currently")]
    public void ItemDataIsSameAfterSave()
    {
        bool result = FileComparison.FileCompare(_itemsFile, _testBackupFile);
        Assert.That(result, Is.True);
    }

	[OneTimeTearDown]
	public void TearDown() => File.Copy(_testBackupFile, _itemsFile, true);
}
