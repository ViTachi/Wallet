using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class AccountView : MonoBehaviour, ISelectable<AccountView>
{
    [SerializeField] private Button button;
    [SerializeField] private IconView iconView;
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private TMPro.TMP_Text balanceText;
    [SerializeField] private Image currencyImage;
    [SerializeField] private Image iconBackgroundImage;
    [SerializeField] private RectTransform selectorIcon;

    public bool IsSelected => selectorIcon.gameObject.activeSelf;

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
            iconView.SetIcon(iconData.Icon);
        nameText.text = account.Name;
        Currency currency = appSettings.CurrenciesSettings.GetCurrency(account.Currency);
        if (currency != null)
            SetAmount(currency, account.Balance);
        IconColor iconColor = appSettings.GetIconColor(account.IconColorType);
        if (iconColor != null)
            iconBackgroundImage.color = iconColor.Color;
    }

    public void SetAmount(Currency currency, float balance)
    {
        currencyImage.gameObject.SetActive(false);
        balanceText.text = $"{string.Format("{0:0.##}", balance)} {(!string.IsNullOrEmpty(currency.IconText) ? currency.IconText: "")}"; 
    }

    public void SetIsSelected(bool selected)
    {
        selectorIcon.gameObject.SetActive(selected);
    }
}
