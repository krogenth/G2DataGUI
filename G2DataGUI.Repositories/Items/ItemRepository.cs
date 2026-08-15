using G2DataGUI.Domain.Items;
using G2DataGUI.Serialization.Items;

namespace G2DataGUI.Repositories.Items;

public sealed class ItemRepository(string filePath)
	: Repository<Item, int>("Items", filePath, new ItemReader(), new ItemWriter());
