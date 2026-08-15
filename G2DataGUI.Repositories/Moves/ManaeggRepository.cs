using G2DataGUI.Domain.Moves;
using G2DataGUI.Serialization.Moves;

namespace G2DataGUI.Repositories.Moves;

public sealed class ManaeggRepository(string filePath)
	: Repository<Manaegg, int>("Manaeggs", filePath, new ManaeggReader(), new ManaeggWriter());
