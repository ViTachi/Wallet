using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CategoriesSettings
{
    [JsonProperty, SerializeField, ArrayElements(Category.TitleProperty)]
    private List<Category> categories;
    [JsonIgnore, SerializeField, ArrayElements(CategoryIcon.TitleProperty)]
    private List<CategoryIcon> categoryIcons;

    [JsonIgnore, SerializeField] public IReadOnlyList<Category> Categories => categories;
    [JsonIgnore] public IReadOnlyList<CategoryIcon> CategoryIcons => categoryIcons;

    public Category GetCategory(CategoryType categoryType) =>
        categories.Find(x => x.CategoryType == categoryType);
    public CategoryIcon GetCategoryIcon(CategoryType iconType) =>
        categoryIcons.Find(x => x.CategoryIconType == iconType);
}
