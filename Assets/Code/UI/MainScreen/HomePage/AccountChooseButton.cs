using UnityEngine;

public class AccountChooseButton : MonoBehaviour
{
    [SerializeField] private IconView iconView; 
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private ScalableButton button;

    private void Awake()
    {
        UpdateView();
        SaveData.Instance.AccountsData.OnSelectAccount += UpdateView;
        button.OnClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UIScreens.Instance.ShowElement<SelectAccountPanel>();
    }

    private void UpdateView()
    {
        Account account = SaveData.Instance.AccountsData.SelectedAccount;
        if (account == null) return;

        AccountIcon accountIcon = AppSettingsData.Instance.AccountsSettings.GetAccountIcon(account.Icon);
        if (accountIcon != null)
            iconView.SetIcon(accountIcon.Icon);
        IconColor iconColor = AppSettingsData.Instance.GetIconColor(account.IconColorType);
        if (iconColor != null)
            iconView.SetColor(iconColor.Color);
        nameText.text = account.Name;
    }
}
