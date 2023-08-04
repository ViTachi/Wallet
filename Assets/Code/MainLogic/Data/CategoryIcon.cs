using UnityEngine;

[System.Serializable]
public class CategoryIcon : IconData
{
    [SerializeField] private CategoryType categoryIconType;
    public CategoryType CategoryIconType => categoryIconType;
}