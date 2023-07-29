using UnityEngine;

public class AccountChooseButton : MonoBehaviour
{
    [SerializeField] private IconView iconView; 
    [SerializeField] private TMPro.TMP_Text nameText;

    private void Awake()
    {
        UpdateView();
        SaveData.Instance.AccountsData.OnSelectAccount += UpdateView;
    }

    private void UpdateView()
    {
        Account account = SaveData.Instance.AccountsData.SelectedAccount;
        if (account == null) return;

        AccountIcon accountIcon = AppSettingsData.Instance.AccountsSettings.GetAccountIcon(account.Icon);
        if (accountIcon != null)
            iconView.SetIcon(accountIcon.Icon);
        Color color = AppSettingsData.Instance.AccountsSettings.GetIconColor(account.IconColorIndex);
        if (color != default)
            iconView.SetColor(color);
        nameText.text = account.Name;
    }
}
