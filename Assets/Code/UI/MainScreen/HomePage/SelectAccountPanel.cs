using System.Collections.Generic;
using UnityEngine;

public class SelectAccountPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;   
    [SerializeField] private RectTransform accountsParent;   
    [SerializeField] private AccountView accountViewPrefab;
    [SerializeField] private ScalableButton createAccountButton;

    private Dictionary<AccountView, Account> accounts = new Dictionary<AccountView, Account>();

    private void Awake()
    {
        SetAccounts();
        SaveData.Instance.AccountsData.OnAccountsChanged += SetAccounts;
        createAccountButton.OnClick.AddListener(OnCreateAccountClicked);
    }

    private void SetAccounts()
    {
        ClearAccounts();
        accountViewPrefab.gameObject.SetActive(false);
        string selectedAccountId = SaveData.Instance.AccountsData.SelectedAccountId;

        foreach (var account in SaveData.Instance.AccountsData.Accounts)
        {
            AccountView accountView = Instantiate(accountViewPrefab, accountsParent);
            accountView.Initialize(account);
            accountView.SetIsSelected(account.Id == selectedAccountId);
            accountView.OnSelected += OnSelectAccount;
            accountView.gameObject.SetActive(true);
            accounts.Add(accountView, account);
        }       
    }

    private void OnSelectAccount(AccountView accountView)
    {
        SaveData.Instance.AccountsData.SelectedAccountId = accounts[accountView].Id;
        foreach (var account in accounts)
        {
            account.Key.SetIsSelected(SaveData.Instance.AccountsData.SelectedAccountId == account.Value.Id);
        }
        Hide(false);
    }

    private void OnCreateAccountClicked()
    {
        CreateAccountPanel createAccountPanel = UIScreens.Instance.GetElement<CreateAccountPanel>();
        createAccountPanel.Show();
        createAccountPanel.SetMode(CreateAccountPanel.Mode.Create);
    }

    private void ClearAccounts()
    {
        foreach (var view in accounts)
        {
            view.Key.OnSelected -= OnSelectAccount;
            Destroy(view.Key.gameObject);
        }
        accounts.Clear();
    }

    public void Show(bool immediately = false)
    {
        SetAccounts();
        bottomPanel.Show(immediately);
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }
}
