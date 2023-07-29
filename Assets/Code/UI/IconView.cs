using UnityEngine;
using UnityEngine.UI;

public class IconView : MonoBehaviour
{
    [SerializeField] private Image iconBackground;
    [SerializeField] private Image iconColored;

    public void Initialize(Sprite icon, Color color)
    {
        iconBackground.sprite = icon;
        iconColored.sprite = icon;
        iconColored.color = color;
    }

    public void SetIcon(Sprite icon)
    {
        iconBackground.sprite = icon;
        iconColored.sprite = icon;
    }

    public void SetColor(Color color)
    {
        iconColored.color = color;
    }
}
