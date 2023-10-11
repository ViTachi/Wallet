using System.Globalization;
using UnityEngine;

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
            string[] parts = account.Balance.ToString("N2", new CultureInfo("en")).Split('.');
            string fractionalPart = parts.Length > 1 ? parts[1] : string.Empty;
            balanceText.text = $"{parts[0]}{((string.IsNullOrEmpty(fractionalPart) || fractionalPart == "00") ? "" : $".<color=#3C3C4366>{fractionalPart}</color>")}";
            currencyText.text = currency.IconText;
        }

        IconColor iconColor = appSettings.GetIconColor(account.IconColorType);
        if (iconColor != null)
            iconView.SetColor(iconColor.Color);
    }
}
