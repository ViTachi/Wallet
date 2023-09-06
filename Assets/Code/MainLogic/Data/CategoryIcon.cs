using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class CategoryIcon : IconData
{
    [JsonIgnore, SerializeField] private CategoryType categoryIconType;
    [JsonIgnore] public CategoryType CategoryIconType => categoryIconType;
    [JsonIgnore] public const string TitleProperty = nameof(categoryIconType);
}