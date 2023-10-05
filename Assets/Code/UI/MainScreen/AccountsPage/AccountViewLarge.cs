using UnityEngine;
using UnityEngine.UI;

public class AccountViewLarge : MonoBehaviour
{
    [SerializeField] private ScalableButton button;
    [SerializeField] private IconView iconView;
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private TMPro.TMP_Text balanceText;
    [SerializeField] private TMPro.TMP_Text currencyText;


    public event System.Action<AccountViewLarge> OnClicked;

    private void Awake()
    {
        button.OnClick.AddListener(() => OnClicked?.Invoke(this));
    }

    public void Initialize(Account account)
    {
        AppSettingsData appSettings = AppSettingsData.Instance;
        IconData iconData = appSettings.AccountsSettings.GetAccountIcon(account.Icon);
        if (iconData != null)
        {
            iconView.SetIcon(iconData.Icon);
        }
        nameText.text = account.Name;
        Currency currency = appSettings.CurrenciesSettings.GetCurrency(account.Currency);
        if (currency != null)
        {
            balanceText.text = account.Balance.ToString();
            currencyText.text = currency.IconText;
        }

        IconColor iconColor = appSettings.AccountsSettings.GetIconColor(account.IconColorType);
        if (iconColor != null)
            iconView.SetColor(iconColor.Color);
    }
}
