using Newtonsoft.Json;

public class SaveData
{
    public static SaveData Instance => AppData.Instance.SaveData;
    public static void Save() => AppData.Instance.Save();


    [JsonProperty] private AccountsSaveData accountsData = new AccountsSaveData();
    [JsonIgnore] public AccountsSaveData AccountsData => accountsData;


    [JsonProperty] private IncomeSourceSaveData incomeSourceData = new IncomeSourceSaveData();
    [JsonIgnore] public IncomeSourceSaveData IncomeSourceData => incomeSourceData;


    [JsonProperty] private OperationsSaveData operationsData = new OperationsSaveData();
    [JsonIgnore] public OperationsSaveData OperationsData => operationsData;
}
