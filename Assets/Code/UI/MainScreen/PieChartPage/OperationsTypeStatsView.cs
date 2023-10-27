using UnityEngine;
using UnityEngine.UI;

public class OperationsTypeStatsView : MonoBehaviour
{
    [SerializeField] private Image iconBackground;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private TMPro.TMP_Text amountText;

    public void Initialize(Color color, Sprite icon, string name, float amount, Currency currency)
    {
        color.a = iconBackground.color.a;
        iconBackground.color = color;
        iconImage.sprite = icon;
        nameText.text = name;
        amountText.text = $"{string.Format("{0:0.##}", amount)} {(!string.IsNullOrEmpty(currency.IconText) ? currency.IconText : "")}";
    }
}
