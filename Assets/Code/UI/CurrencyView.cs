using System;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour, ISelectable<CurrencyView>
{
    [SerializeField] private Button button;
    [SerializeField] private TMPro.TMP_Text currencyNameText;
    [SerializeField] private RectTransform selectorIcon;

    private Currency currency = null;

    public event Action<CurrencyView> OnSelected;
    public bool IsSelected => selectorIcon.gameObject.activeSelf;
    public Currency Currency => currency;
    public string NameText => currencyNameText.text;

    private void Awake()
    {
        button.onClick.AddListener(() => OnSelected?.Invoke(this));
    }

    public void SetIsSelected(bool selected)
    {
        selectorIcon.gameObject.SetActive(selected);
    }

    public void Initialize(CurrencyType currencyType)
    {
        currency = AppSettingsData.Instance.CurrenciesSettings.GetCurrency(currencyType);
        if(currency != null)
        {
            currencyNameText.text = $"{currency.CurrencyName} — {currency.CurrencyFullName}";
        }
    }
}
