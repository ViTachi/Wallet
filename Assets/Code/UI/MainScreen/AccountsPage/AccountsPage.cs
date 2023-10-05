using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AccountsPage : MonoBehaviour
{
    [SerializeField] private RectTransform accountsListPanel;
    [SerializeField] private RectTransform operationsListPanel;
    [Space(20)]
    [SerializeField] private ScalableButton addAccountButton;
    [SerializeField] private RectTransform accountsParent;
    [SerializeField] private AccountViewLarge accountViewPrefab;
    [Space(20)]
    [SerializeField] private DayTransactionsView dayTransactionsViewPrefab;
    [SerializeField] private RectTransform dayTransactionsViewsParent;

    private Dictionary<AccountViewLarge, Account> accounts = new Dictionary<AccountViewLarge, Account>();

    private List<DayTransactionsView> dayTransactionsViews = new List<DayTransactionsView>();

    private void Awake()
    {
        addAccountButton.OnClick.AddListener(OnCreateAccountClicked);
        SaveData.Instance.AccountsData.OnAccountsChanged += SetAccounts;
    }

    private void OnEnable()
    {
        SetAccounts();
        ShowAccountsListPanel(true);
        HideOperationsListPanel(true);
    }

    private void OnDisable()
    {

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
        UIScreens.Instance.ShowElement<CreateAccountPanel>();
    }

    private void OnAccountPressed(AccountViewLarge accountView)
    {
        ClearOperationsList();

        List<IOperation> operations = new List<IOperation>();
        operations.AddRange(SaveData.Instance.OperationsData.ExpenseOperations);
        operations.AddRange(SaveData.Instance.OperationsData.IncomeOperations);
        operations = operations.Where(x => x.AccountId == accounts[accountView].Id).ToList();

        dayTransactionsViewPrefab.gameObject.SetActive(false);

        var days = from o in operations
                   group o by new { o.DateTime.DayOfYear, o.DateTime.Year };

        days = days.OrderByDescending(x => x.FirstOrDefault().DateTime);

        foreach (var day in days)
        {
            DayTransactionsView dayTransactionsView = Instantiate(dayTransactionsViewPrefab, dayTransactionsViewsParent);
            dayTransactionsView.Initialize(day.OrderByDescending(x => x.DateTime).ToList(), accounts[accountView]);
            dayTransactionsView.gameObject.SetActive(true);
            dayTransactionsViews.Add(dayTransactionsView);
        }

        HideAccountsListPanel();
        ShowOperationsListPanel();
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
    }
    private void HideOperationsListPanel(bool immidiately = false)
    {
        operationsListPanel.gameObject.SetActive(false);
    }
    private void ShowOperationsListPanel(bool immidiately = false)
    {
        operationsListPanel.gameObject.SetActive(true);
    }
}
