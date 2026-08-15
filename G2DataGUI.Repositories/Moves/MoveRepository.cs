using G2DataGUI.Domain.Moves;
using G2DataGUI.Serialization.Moves;

namespace G2DataGUI.Repositories.Moves;

public sealed class MoveRepository(string filePath)
	: Repository<Move, int>("Moves", filePath, new MoveReader(), new MoveWriter());
