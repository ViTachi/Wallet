using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CategoriesSettings
{
    [JsonProperty, SerializeField] private List<Category> categories;

    [JsonIgnore, SerializeField] public IReadOnlyList<Category> Categories => categories;

    public Category GetCategory(CategoryType categoryType) => categories.Find(x => x.CategoryType == categoryType);
}
