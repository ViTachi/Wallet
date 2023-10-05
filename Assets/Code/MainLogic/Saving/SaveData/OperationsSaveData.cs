using System.Collections.Generic;
using Newtonsoft.Json;

public class OperationsSaveData
{
    [JsonProperty] private List<ExpenseOperationData> expenseOperations = new List<ExpenseOperationData>();
    [JsonIgnore] public IReadOnlyList<ExpenseOperationData> ExpenseOperations => expenseOperations;

    [JsonProperty] private List<ExpenseOperationData> incomeOperations = new List<ExpenseOperationData>();
    [JsonIgnore] public IReadOnlyList<ExpenseOperationData> IncomeOperations => incomeOperations;

    public void AddOperation(ExpenseOperationData operationData)
    {
        expenseOperations.Add(operationData);
        SaveData.Save();
    }
    public void RemoveOperation(ExpenseOperationData operationData)
    {
        expenseOperations.Remove(operationData);
        SaveData.Save();
    }
}
