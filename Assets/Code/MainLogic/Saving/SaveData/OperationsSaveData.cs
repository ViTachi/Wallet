using System.Collections.Generic;
using Newtonsoft.Json;

public class OperationsSaveData
{
    [JsonProperty] private List<ExpenseOperationData> expenseOperations = new List<ExpenseOperationData>();
    [JsonIgnore] public IReadOnlyList<ExpenseOperationData> ExpenseOperations => expenseOperations;

    [JsonProperty] private List<IncomeOperationData> incomeOperations = new List<IncomeOperationData>();
    [JsonIgnore] public IReadOnlyList<IncomeOperationData> IncomeOperations => incomeOperations;

    public void AddExpenceOperation(ExpenseOperationData operationData)
    {
        expenseOperations.Add(operationData);
        SaveData.Save();
    }

    public void AddIncomeOperation(IncomeOperationData operationData)
    {
        incomeOperations.Add(operationData);
        SaveData.Save();
    }

    public void RemoveOperation(ExpenseOperationData operationData)
    {
        expenseOperations.Remove(operationData);
        SaveData.Save();
    }
}
