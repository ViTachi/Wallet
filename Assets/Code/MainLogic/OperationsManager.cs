using System;

public class OperationsManager : Singleton<OperationsManager>
{
    private OperationData currentOperationData;

    private bool customDate = false;

    public bool IsOperationNoteEmpty => string.IsNullOrEmpty(currentOperationData.Note);

    public string Note => currentOperationData.Note;

    public void SetValue(float value)
    {
        currentOperationData.Value = value;
    }

    public void SetCategory(Category category)
    {
        currentOperationData.CategoryId = category.Id;
    }

    public void SetNote(string note)
    {
        currentOperationData.Note = note;
    }

    public void DeleteNote()
    {
        currentOperationData.Note = "";
    }

    public void SetCustomDate(DateTime dateTime)
    {
        customDate = true;
        currentOperationData.Date = dateTime;
    }

    public void ClearCustomDate()
    {
        customDate = false;
        currentOperationData.Date = default;
    }

    public bool SaveOperation()
    {
        if (currentOperationData.Value == 0 || string.IsNullOrEmpty(currentOperationData.CategoryId))
        {
            return false;
        }
        if (!customDate)
        {
            currentOperationData.Date = System.DateTime.UtcNow;
        }
        DataSaver.SaveData.Operations.Add(currentOperationData);
        DataSaver.Save();
        currentOperationData = new OperationData();
        return true;
    }

    protected override void Setup()
    {
        currentOperationData = new OperationData();
    }
}
