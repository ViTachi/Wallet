using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayTransactionsView : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text dateText;
    [SerializeField] private TMPro.TMP_Text differenceText;
    [SerializeField] private RectTransform transactionsParent;
    [SerializeField] private TransactionView transactionViewPrefab;

    private List<TransactionView> transactions = new List<TransactionView>();

    public void Initialize(List<IOperation> operations, Account account)
    {
        ClearTransactions();
        if (operations == null || operations.Count < 1) return;

        var currency = AppSettingsData.Instance.CurrenciesSettings.GetCurrency(account.Currency);
        if (currency != null)
            differenceText.text = $"{operations.Sum(x => x.Value)} {currency.IconText}";

        DateTime date = operations[0].DateTime;
        dateText.text = $"{date.Day}.{date.Month}.{date.Year}";

        transactionViewPrefab.gameObject.SetActive(false);
        foreach (var operation in operations)
        {
            TransactionView transactionView = Instantiate(transactionViewPrefab, transactionsParent);
            transactionView.gameObject.SetActive(true);
            transactionView.Initialize(account.Currency, operation.Icon, operation.Name,
                operation is ExpenseOperationData ? -operation.Value : operation.Value);
            transactions.Add(transactionView);
        }
    }

    private void ClearTransactions()
    {
        foreach (var transaction in transactions)
        {
            Destroy(transaction.gameObject);
        }
        transactions.Clear();
    }
}
