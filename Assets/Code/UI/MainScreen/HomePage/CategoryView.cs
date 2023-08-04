using System;
using UnityEngine;
using UnityEngine.UI;

public class CategoryView : MonoBehaviour, ISelectable<CategoryView>
{
    [SerializeField] private ScalableButton button;
    [SerializeField] private Image categoryIcon;
    [SerializeField] private Graphic background;
    [SerializeField] private TMPro.TMP_Text categoryName;

    public bool IsSelected => false;

    public event Action<CategoryView> OnSelected;

    private void Awake()
    {
        button.OnClick.AddListener(() => OnSelected?.Invoke(this));
    }

    public void Initialize(CategoryType categoryType)
    {
        CategoryIcon icon = AppSettingsData.Instance.GetCategoryIcon(categoryType);
        if (icon != null)
            categoryIcon.sprite = icon.Icon;
        Category category = AppSettingsData.Instance.CategoriesSettings.GetCategory(categoryType);
        if (category != null)
        {
            categoryName.text = category.CategoryName;
            background.color = category.CategoryColor;
        }
    }

    public void SetIsSelected(bool selected)
    {
        
    }
}
