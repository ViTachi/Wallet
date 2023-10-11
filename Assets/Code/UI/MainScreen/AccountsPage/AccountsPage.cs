using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccountsPage : MonoBehaviour
{
    [SerializeField] private RectTransform accountsListPanel;
    [SerializeField] private RectTransform operationsListPanel;
    [SerializeField] private Graphic background;
    [SerializeField] private Color accountsListBackgroundColor = Color.white;
    [SerializeField] private Color operationsListBackgroundColor = Color.white;
    [Space(20)]
    [SerializeField] private ScalableButton addAccountButton;
    [SerializeField] private RectTransform accountsParent;
    [SerializeField] private AccountViewLarge accountViewPrefab;
    [Space(20)]
    [SerializeField] private DayTransactionsView dayTransactionsViewPrefab;
    [SerializeField] private RectTransform dayTransactionsViewsParent;
    [SerializeField] private TMPro.TMP_Text accountNameText;
    [SerializeField] private TMPro.TMP_Text accountBalanceText;
    [SerializeField] private TMPro.TMP_Text currencyText;
    [SerializeField] private AccountIconView accountIcon;
    [SerializeField] private ScalableButton backButton;
    [SerializeField] private ScalableButton editButton;

    private Dictionary<AccountViewLarge, Account> accounts = new Dictionary<AccountViewLarge, Account>();

    private List<DayTransactionsView> dayTransactionsViews = new List<DayTransactionsView>();

    private Account selectedAccount = null;

    private CreateAccountPanel createAccountPanel = null;

    private void Awake()
    {
        addAccountButton.OnClick.AddListener(OnCreateAccountClicked);
        SaveData.Instance.AccountsData.OnAccountsChanged += SetAccounts;
        backButton.OnClick.AddListener(OnBackButtonClicked);
        editButton.OnClick.AddListener(OnEditButtonClicked);
    }

    private void OnEnable()
    {
        SetAccounts();
        ShowAccountsListPanel(true);
        HideOperationsListPanel(true);
        selectedAccount = null;

        createAccountPanel = UIScreens.Instance.GetElement<CreateAccountPanel>();
        if (createAccountPanel != null)
            createAccountPanel.OnAccountDataChanged += OnAccountDataChanged;
    }

    private void OnDisable()
    {
        if(createAccountPanel != null)
            createAccountPanel.OnAccountDataChanged -= OnAccountDataChanged;
    }

    private void SetAccounts()
    {
        RemoveAccountViews();
        accountViewPrefab.gameObject.SetActive(false);
        foreach (var account in SaveData.Instance.AccountsData.Accounts)
        {
            AccountViewLarge accountView = Instantiate(accountViewPrefab, accountsParent);
            accountView.Initialize(account);
            accountView.gameObject.SetActive(true);
            accountView.OnClicked += OnAccountPressed;
            accounts.Add(accountView, account);
        }
    }

    private void RemoveAccountViews()
    {
        foreach (var account in accounts)
        {
            account.Key.OnClicked -= OnAccountPressed;
            Destroy(account.Key.gameObject);
        }
        accounts.Clear();
    }

    private void OnCreateAccountClicked()
    {
        CreateAccountPanel createAccountPanel = UIScreens.Instance.GetElement<CreateAccountPanel>();
        createAccountPanel.Show();
        createAccountPanel.SetMode(CreateAccountPanel.Mode.Create);
    }

    private void OnEditButtonClicked()
    {
        CreateAccountPanel createAccountPanel = UIScreens.Instance.GetElement<CreateAccountPanel>();
        createAccountPanel.Show();
        createAccountPanel.SetMode(CreateAccountPanel.Mode.Edit);
        createAccountPanel.SetAccountForEdit(selectedAccount);
    }

    private void OnAccountDataChanged(Account account)
    {
        if (SaveData.Instance.AccountsData.GetAccount(account.Id) == null)
            OnBackButtonClicked();
        else
            SetAccountData(account);
    }

    private void OnBackButtonClicked()
    {
        ShowAccountsListPanel(false);
        HideOperationsListPanel(false);
        selectedAccount = null;
        SetAccounts();
    }

    private void OnAccountPressed(AccountViewLarge accountView)
    {
        if (!accounts.ContainsKey(accountView)) return;

        selectedAccount = accounts[accountView];

        SetAccountData(selectedAccount);

        HideAccountsListPanel();
        ShowOperationsListPanel();
    }

    private void SetAccountData(Account account)
    {
        ClearOperationsList();

        List<IOperation> operations = new List<IOperation>();
        operations.AddRange(SaveData.Instance.OperationsData.ExpenseOperations);
        operations.AddRange(SaveData.Instance.OperationsData.IncomeOperations);
        operations = operations.Where(x => x.AccountId == account.Id).ToList();

        dayTransactionsViewPrefab.gameObject.SetActive(false);

        var days = from o in operations
                   group o by new { o.DateTime.DayOfYear, o.DateTime.Year };

        days = days.OrderByDescending(x => x.FirstOrDefault().DateTime);

        foreach (var day in days)
        {
            DayTransactionsView dayTransactionsView = Instantiate(dayTransactionsViewPrefab, dayTransactionsViewsParent);
            dayTransactionsView.Initialize(day.OrderByDescending(x => x.DateTime).ToList(), account);
            dayTransactionsView.gameObject.SetActive(true);
            dayTransactionsViews.Add(dayTransactionsView);
        }

        accountNameText.text = account.Name;
        accountBalanceText.text = account.Balance.ToString();
        Currency currency = AppSettingsData.Instance.CurrenciesSettings.GetCurrency(account.Currency);
        if (currency != null)
        {
            currencyText.text = currency.IconText;
        }
        accountIcon.Initialize(account.Icon, account.IconColorType);
    }

    private void ClearOperationsList()
    {
        foreach (var dayTransactionsView in dayTransactionsViews)
        {
            Destroy(dayTransactionsView.gameObject);
        }
        dayTransactionsViews.Clear();
    }

    private void HideAccountsListPanel(bool immidiately = false)
    {
        accountsListPanel.gameObject.SetActive(false);
    }
    private void ShowAccountsListPanel(bool immidiately = false)
    {
        accountsListPanel.gameObject.SetActive(true);
        background.color = accountsListBackgroundColor;
    }
    private void HideOperationsListPanel(bool immidiately = false)
    {
        operationsListPanel.gameObject.SetActive(false);
    }
    private void ShowOperationsListPanel(bool immidiately = false)
    {
        operationsListPanel.gameObject.SetActive(true);
        background.color = operationsListBackgroundColor;
    }
}
