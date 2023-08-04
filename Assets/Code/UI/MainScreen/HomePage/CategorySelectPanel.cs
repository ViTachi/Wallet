using System;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelectPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton editButton;

    [SerializeField] private CategoryView categoryViewPrefab;
    [SerializeField] private RectTransform categoriesParent;

    private Dictionary<CategoryView, CategoryType> views = new Dictionary<CategoryView, CategoryType>();

    public Action<CategoryType> OnChooseCategory;

    private void Awake()
    {
        editButton.OnClick.AddListener(OnEdit);
    }

    private void OnEdit()
    {

    }

    private void OnCategorySelected(CategoryView view)
    {
        OnChooseCategory?.Invoke(views[view]);
        Hide(false);
    }

    private void SetupCategories()
    {
        ClearCategories();

        categoryViewPrefab.gameObject.SetActive(false);

        foreach (var category in AppSettingsData.Instance.CategoriesSettings.Categories)
        {
            CategoryView categoryView = Instantiate(categoryViewPrefab, categoriesParent);
            categoryView.gameObject.SetActive(true);
            categoryView.Initialize(category.CategoryType);
            categoryView.OnSelected += OnCategorySelected;
            views[categoryView] = category.CategoryType;
        }
    }

    private void ClearCategories()
    {
        foreach (var view in views)
        {
            Destroy(view.Key.gameObject);
        }
        views.Clear();
    }


    public void Show(bool immediately = false)
    {
        bottomPanel.Show(immediately);

        SetupCategories();
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }
}
