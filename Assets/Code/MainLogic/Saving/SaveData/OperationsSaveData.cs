using System.Collections.Generic;
using Newtonsoft.Json;

public class OperationsSaveData
{
    [JsonProperty] private List<ExpenseOperationData> operations = new List<ExpenseOperationData>();
    [JsonIgnore] public IReadOnlyList<ExpenseOperationData> Operations => operations;

    public void AddOperation(ExpenseOperationData operationData) => operations.Add(operationData);
    public void RemoveOperation(ExpenseOperationData operationData) => operations.Remove(operationData);
}
