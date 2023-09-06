using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using UnityEngine;

[Serializable]
public class IconColor
{
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private IconColorType iconColorType;
    [JsonConverter(typeof(ColorJSONConverter))]
    [JsonProperty, SerializeField] private Color color = Color.white;
    [JsonIgnore] public IconColorType IconColorType => iconColorType;
    [JsonIgnore] public Color Color => color;
    [JsonIgnore] public const string TitleProperty = nameof(iconColorType);
}

public enum IconColorType
{
    None,
    ModerateBlue,
    Red,
    Orange,
    Yellow,
    SoftBlue,
    Blue,
    Green,
    Violet,
    VividRed,
    Black,
}