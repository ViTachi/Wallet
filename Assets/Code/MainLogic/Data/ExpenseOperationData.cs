using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class ExpenseOperationData
{
    [JsonProperty] public float Value { get; private set; }
    [JsonProperty] public string AccountId { get; private set; }
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty] public CategoryType CategoryType { get; private set; }
    [JsonProperty] public DateTime Date { get; private set; }
    [JsonProperty] public string Note { get; private set; }

    public void SetValue(float value) => Value = value;
    public void SetAccountId(string accountId) => AccountId = accountId;
    public void SetCategoryType(CategoryType categoryType) => CategoryType = categoryType;
    public void SetDate(DateTime date) => Date = date;
    public void SetNote(string note) => Note = note;
}
