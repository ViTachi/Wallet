using UnityEngine;

[CreateAssetMenu(menuName = "Data/Category", fileName = "Category")]
public class Category : ScriptableObject
{
    public string Id { get; set; }
    public Sprite Icon { get; set; }
}