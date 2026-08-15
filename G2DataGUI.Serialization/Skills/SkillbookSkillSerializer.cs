using G2DataGUI.Domain.Skills;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Skills;

public static class SkillbookSkillSerializer
{
	public static SkillbookSkill Read(Stream stream) => new()
	{
		SkillOffset = stream.ReadRawByte(),
		StartingLevel = stream.ReadRawByte(),
		RequiredLevel = stream.ReadRawByte(),
		Unknown1 = stream.ReadRawByte(),
	};

	public static void Write(Stream stream, SkillbookSkill skill)
	{
		stream.WriteRawByte(skill.SkillOffset);
		stream.WriteRawByte(skill.StartingLevel);
		stream.WriteRawByte(skill.RequiredLevel);
		stream.WriteRawByte(skill.Unknown1);
	}
}
