using UnityEngine;
using UnityEngine.UI;

public class AccountChooseButton : MonoBehaviour
{
    [SerializeField] private Image icon; 
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
        if (accountIcon)
            icon.sprite = accountIcon.Icon;
        nameText.text = account.Name;
    }
}
