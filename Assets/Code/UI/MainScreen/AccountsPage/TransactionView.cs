using UnityEngine;
using UnityEngine.UI;

public class TransactionView : MonoBehaviour
{
    [SerializeField] private Image _categoryIcon;
    [SerializeField] private TMPro.TMP_Text categoryName;
    [SerializeField] private TMPro.TMP_Text differenceText;
    [SerializeField] private RectTransform separatorLine;
    [SerializeField] private Color incomeTextColor;
    [SerializeField] private Color expenseTextColor;

    public void SetSeparatorActive(bool active)
    {
        separatorLine.gameObject.SetActive(active);
    }

    public void Initialize(CurrencyType currencyType, Sprite icon, string name, float difference)
    {
        differenceText.color = difference > 0 ? incomeTextColor : expenseTextColor;
        var currency = AppSettingsData.Instance.CurrenciesSettings.GetCurrency(currencyType);
        if (currency != null)
            differenceText.text = $"{difference} {currency.IconText}";
        categoryName.text = name;
        _categoryIcon.sprite = icon;
    }
}
