using System;

namespace G2DataGUI.Common.Extensions;

public static class EnumExtensions
{
	public static T ToEnum<T>(this string enumString) =>
		(T)Enum.Parse(typeof(T), enumString);

	public static bool EnumExists<T>(this byte enumValue) where T : Enum =>
		Enum.IsDefined(typeof(T), enumValue);

	public static T ToEnum<T>(this byte enumValue) where T : Enum
	{
		if (Enum.IsDefined(typeof(T), enumValue))
		{
			return (T)(object)enumValue;
		}

		throw new ArgumentOutOfRangeException(enumValue.ToString());
	}
}
