using G2DataGUI.Domain.Moves;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Moves;

public static class ManaeggSpellSerializer
{
	public static ManaeggSpell Read(Stream stream) => new()
	{
		SpellOffset = stream.ReadRawByte(),
		StartingLevel = stream.ReadRawByte(),
		RequiredLevel = stream.ReadRawByte(),
		Unknown1 = stream.ReadRawByte(),
	};

	public static void Write(Stream stream, ManaeggSpell spell)
	{
		stream.WriteRawByte(spell.SpellOffset);
		stream.WriteRawByte(spell.StartingLevel);
		stream.WriteRawByte(spell.RequiredLevel);
		stream.WriteRawByte(spell.Unknown1);
	}
}
