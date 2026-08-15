using G2DataGUI.Domain.Specials;
using G2DataGUI.Serialization.Specials;

namespace G2DataGUI.Repositories.Specials;

public sealed class SpecialSetRepository(string filePath)
	: Repository<SpecialSet, int>("Specials", filePath, new SpecialSetReader(), new SpecialSetWriter());
