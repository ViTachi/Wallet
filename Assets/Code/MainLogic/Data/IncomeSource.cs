using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[System.Serializable]
public class IncomeSource
{
    [JsonProperty, SerializeField] private string id;
    [JsonProperty, SerializeField] private string name;
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private IncomeSourceIconType icon;

    [JsonIgnore] public string Id => id;
    [JsonIgnore] public string Name => name;
    [JsonIgnore] public IncomeSourceIconType Icon => icon;

    public IncomeSource(string id, string name, IncomeSourceIconType icon)
    {
        SetId(id);
        SetName(name);
        SetIcon(icon);
    }

    public void SetId(string id) => this.id = id;
    public void SetName(string name) => this.name = name;
    public void SetIcon(IncomeSourceIconType icon) => this.icon = icon;
}
