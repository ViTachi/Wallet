using Newtonsoft.Json;
using System;
using UnityEngine;

public class ColorJSONConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(Color));
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(ColorUtility.ToHtmlStringRGBA((Color)value));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (ColorUtility.TryParseHtmlString($"#{reader.Value}", out Color color))
            return color;
        return default;
    }
}