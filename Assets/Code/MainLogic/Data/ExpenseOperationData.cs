using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public class ExpenseOperationData : IOperation
{
    [JsonProperty] public float Value { get; private set; }
    [JsonProperty] public string AccountId { get; private set; }
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty] public CategoryType CategoryType { get; private set; }
    [JsonProperty] public DateTime DateTime { get; private set; }
    [JsonProperty] public string Note { get; private set; }

    [JsonIgnore] public Sprite Icon => CategoryType != CategoryType.None ? 
        AppSettingsData.Instance.CategoriesSettings.GetCategoryIcon(CategoryType).Icon : default;
    [JsonIgnore] public string Name => CategoryType != CategoryType.None ?
        AppSettingsData.Instance.CategoriesSettings.GetCategory(CategoryType).CategoryName : default;

    public void SetValue(float value) => Value = value;
    public void SetAccountId(string accountId) => AccountId = accountId;
    public void SetCategoryType(CategoryType categoryType) => CategoryType = categoryType;
    public void SetDate(DateTime date) => DateTime = date;
    public void SetNote(string note) => Note = note;
}
