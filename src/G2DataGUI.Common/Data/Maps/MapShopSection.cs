using System.IO;
using G2DataGUI.Common.Data.Common;

namespace G2DataGUI.Common.Data.Maps;

public class MapShopSection : BaseContainer
{
    private FixedLengthTitle _title;
    public MapShopItemList Items { get; set; }

	public string Title
	{
		get => _title.Title;
		set
		{
			_title.Title = value;
			NotifyPropertyChanged(nameof(Title));
		}
	}

	public static MapShopSection ReadMapShopSection(Stream reader)
	{
		MapShopSection section = new()
		{
			_title = FixedLengthTitle.ReadFixedLengthTitle(reader),
			Items = MapShopItemList.ReadMapShopItemList(reader),
		};
		return section;
	}

	public void WriteMapShopSection(Stream writer)
	{
		_title.WriteFixedLengthTitle(writer);
		Items.WritemapShopItemList(writer);
	}
}
