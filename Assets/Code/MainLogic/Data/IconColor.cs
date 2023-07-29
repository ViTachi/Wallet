using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class IconColor
{
    [JsonProperty, SerializeField] private IconColorType iconColorType;
    [JsonProperty, SerializeField] private Color color = Color.white;
    [JsonIgnore] public IconColorType IconColorType => iconColorType;
    [JsonIgnore] public Color Color => color;
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
