using UnityEngine;

public class IconData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
}
