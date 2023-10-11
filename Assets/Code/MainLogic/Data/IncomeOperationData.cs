using System;
using Newtonsoft.Json;
using UnityEngine;

public class IncomeOperationData : IOperation
{
    [JsonProperty] public float Value { get; private set; }
    [JsonProperty] public string IncomeSourceId { get; private set; }
    [JsonProperty] public string AccountId { get; private set; }
    [JsonProperty] public DateTime DateTime { get; private set; }
    [JsonProperty] public string Note { get; private set; }

    [JsonIgnore] public Sprite Icon => AppSettingsData.Instance.IncomeSourceSettings
        .GetIncomeSourceIcon(SaveData.Instance.IncomeSourceData.GetIncomeSource(IncomeSourceId).Icon).Icon;
    [JsonIgnore] public string Name => SaveData.Instance.IncomeSourceData.GetIncomeSource(IncomeSourceId).Name;

    public void SetValue(float value) => Value = value;
    public void SetIncomeSourceId(string incomeSourceId) => IncomeSourceId = incomeSourceId;
    public void SetAccountId(string accountId) => AccountId = accountId;
    public void SetDate(DateTime date) => DateTime = date;
    public void SetNote(string note) => Note = note;
}