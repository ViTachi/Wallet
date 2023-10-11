using System;

public class OperationsManager : Singleton<OperationsManager>
{
    private ExpenseOperationData currentExpenseOperationData;
    private IncomeOperationData currentIncomeOperationData;


    private bool isCustomDate = false;
    public bool IsCustomDate => isCustomDate;

    public bool IsOperationNoteEmpty => string.IsNullOrEmpty(currentExpenseOperationData.Note);

    public string Note => currentExpenseOperationData.Note;

    public void SetValue(float value)
    {
        currentExpenseOperationData.SetValue(-value);
        currentIncomeOperationData.SetValue(value);
    }

    public void SetCurrentAccount(Account account)
    {
        currentExpenseOperationData.SetAccountId(account.Id);
        currentIncomeOperationData.SetAccountId(account.Id);
    }

    public void SetCategoryType(CategoryType categoryType)
    {
        currentExpenseOperationData.SetCategoryType(categoryType);
    }

    public void SetCurrentIncome(IncomeSource incomeSource)
    {
        currentIncomeOperationData.SetIncomeSourceId(incomeSource.Id);
    }

    public void SetNote(string note)
    {
        currentExpenseOperationData.SetNote(note);
        currentIncomeOperationData.SetNote(note);
    }

    public void DeleteNote()
    {
        currentExpenseOperationData.SetNote(string.Empty);
        currentIncomeOperationData.SetNote(string.Empty);
    }

    public void SetCustomDate(DateTime dateTime)
    {
        isCustomDate = true;
        currentExpenseOperationData.SetDate(dateTime);
        currentIncomeOperationData.SetDate(dateTime);
    }

    public void ClearCustomDate()
    {
        isCustomDate = false;
        currentExpenseOperationData.SetDate(default);
        currentIncomeOperationData.SetDate(default);
    }

    public bool SaveExpenceOperation()
    {
        if (currentExpenseOperationData.Value == 0 || string.IsNullOrEmpty(currentExpenseOperationData.AccountId))
        {
            return false;
        }
        if (!isCustomDate)
        {
            currentExpenseOperationData.SetDate(DateTime.Now);
        }
        Account account = SaveData.Instance.AccountsData.GetAccount(currentExpenseOperationData.AccountId);
        if (account != null)
            account.Income(currentExpenseOperationData.Value);
        SaveData.Instance.OperationsData.AddExpenceOperation(currentExpenseOperationData);
        SaveData.Save();
        currentExpenseOperationData = new ExpenseOperationData();
        currentIncomeOperationData = new IncomeOperationData();
        return true;
    }


    public bool SaveIncomeOperation()
    {
        if (currentIncomeOperationData.Value == 0 || string.IsNullOrEmpty(currentIncomeOperationData.AccountId)
            || string.IsNullOrEmpty(currentIncomeOperationData.IncomeSourceId))
        {
            return false;
        }
        if (!isCustomDate)
        {
            currentIncomeOperationData.SetDate(DateTime.Now);
        }
        Account account = SaveData.Instance.AccountsData.GetAccount(currentIncomeOperationData.AccountId);
        if (account != null)
            account.Income(currentIncomeOperationData.Value);
        SaveData.Instance.OperationsData.AddIncomeOperation(currentIncomeOperationData);
        SaveData.Save();
        currentExpenseOperationData = new ExpenseOperationData();
        currentIncomeOperationData = new IncomeOperationData();
        return true;
    }


    protected override void Setup()
    {
        currentExpenseOperationData = new ExpenseOperationData();
        currentIncomeOperationData = new IncomeOperationData();
    }
}
