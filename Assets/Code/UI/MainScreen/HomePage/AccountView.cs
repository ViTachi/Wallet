using UnityEngine;
using UnityEngine.UI;

public class AccountView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private TMPro.TMP_Text balanceText;
    [SerializeField] private Image currencyImage;
    [SerializeField] private RectTransform selectorIcon;

    public event System.Action<AccountView> OnSelected;

    private void Awake()
    {
        button.onClick.AddListener(() => OnSelected?.Invoke(this));
    }

    public void Initialize (Account account)
    {
        AppSettingsData appSettings = AppSettingsData.Instance;
        IconData iconData = appSettings.AccountsSettings.GetAccountIcon(account.Icon);
        if(iconData != null)
            iconImage.sprite = iconData.Icon;
        nameText.text = account.Name;
        Currency currency = appSettings.CurrenciesSettings.GetCurrency(account.Currency);
        if (currency != null)
            SetAmount(currency, account.Balance);
    }

    public void SetAmount(Currency currency, float balance)
    {
        currencyImage.gameObject.SetActive(currency.Icon != null);
        if (currency.Icon != null)
            currencyImage.sprite = currency.Icon;
        balanceText.text = $"{balance} {(!string.IsNullOrEmpty(currency.IconText) ? currency.IconText: "")}"; 
    }

    public void SetIsSelected(bool selected)
    {
        selectorIcon.gameObject.SetActive(selected);
    }
}
