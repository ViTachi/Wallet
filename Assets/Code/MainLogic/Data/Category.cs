using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Category
{
    [JsonProperty, SerializeField] private CategoryType categoryType;
    [JsonProperty, SerializeField] private string categoryName;
    [JsonConverter(typeof(ColorJSONConverter))]
    [JsonProperty, SerializeField] private Color categoryColor = Color.white;
    [JsonIgnore] public CategoryType CategoryType => categoryType;
    [JsonIgnore] public string CategoryName => categoryName;
    [JsonIgnore] public Color CategoryColor => categoryColor;
}

public enum CategoryType
{
    None,
    Pets,
    Entertainment,
    Food,
    Health,
    Hygiene,
    Clothes,
    Sport,
    Gifts,
    Home,
    Bills,
    Transport,
}
