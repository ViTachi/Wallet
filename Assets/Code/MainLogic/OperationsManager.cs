using System;

public class OperationsManager : Singleton<OperationsManager>
{
    private ExpenseOperationData currentOperationData;
    public ExpenseOperationData CurrentOperationData => currentOperationData;

    private bool isCustomDate = false;
    public bool IsCustomDate => isCustomDate;

    public bool IsOperationNoteEmpty => string.IsNullOrEmpty(currentOperationData.Note);

    public string Note => currentOperationData.Note;

    public void SetValue(float value)
    {
        currentOperationData.SetValue(value);
    }

    public void SetCurrentAccount(Account account)
    {
        currentOperationData.SetAccountId(account.Id);
    }

    public void SetNote(string note)
    {
        currentOperationData.SetNote(note);
    }

    public void DeleteNote()
    {
        currentOperationData.SetNote(string.Empty);
    }

    public void SetCustomDate(DateTime dateTime)
    {
        isCustomDate = true;
        currentOperationData.SetDate(dateTime);
    }

    public void ClearCustomDate()
    {
        isCustomDate = false;
        currentOperationData.SetDate(default);
    }

    public bool SaveOperation()
    {
        if (currentOperationData.Value == 0 || string.IsNullOrEmpty(currentOperationData.AccountId))
        {
            return false;
        }
        if (!isCustomDate)
        {
            currentOperationData.SetDate(DateTime.UtcNow);
        }
        SaveData.Instance.OperationsData.AddOperation(currentOperationData);
        SaveData.Save();
        currentOperationData = new ExpenseOperationData();
        return true;
    }

    protected override void Setup()
    {
        currentOperationData = new ExpenseOperationData();
    }
}
