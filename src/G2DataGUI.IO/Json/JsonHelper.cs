﻿using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace G2DataGUI.IO.Json;

public class JsonHelper
{
	public static string Serialize<T>(T value, JsonTypeInfo<T> typeInfo) => JsonSerializer.Serialize(value, typeInfo);
	public static T? Deserialize<T>(string value, JsonTypeInfo<T> typeInfo) => JsonSerializer.Deserialize(value, typeInfo);
}
