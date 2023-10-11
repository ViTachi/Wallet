using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using UnityEngine;

[System.Serializable]
public class IncomeSource : ICloneable
{
    [JsonProperty, SerializeField] private string id;
    [JsonProperty, SerializeField] private string name;
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private IncomeSourceIconType icon;
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private IconColorType iconColorType;

    [JsonIgnore] public string Id => id;
    [JsonIgnore] public string Name => name;
    [JsonIgnore] public IncomeSourceIconType Icon => icon;
    [JsonIgnore] public IconColorType IconColorType => iconColorType;

    public IncomeSource(string id, string name, IncomeSourceIconType icon, IconColorType iconColorType)
    {
        SetId(id);
        SetName(name);
        SetIcon(icon);
        SetIconColorType(iconColorType);
    }

    public void SetId(string id) => this.id = id;
    public void SetName(string name) => this.name = name;
    public void SetIcon(IncomeSourceIconType icon) => this.icon = icon;
    public void SetIconColorType(IconColorType iconColorType) => this.iconColorType = iconColorType;

    public object Clone()
    {
        return MemberwiseClone();
    }
}
