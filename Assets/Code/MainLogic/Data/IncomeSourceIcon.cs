using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using UnityEngine;

[Serializable]
public class IncomeSourceIcon : IconData
{
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonIgnore, SerializeField] private IncomeSourceIconType incomeSourceIconType;
    [JsonIgnore] public IncomeSourceIconType IncomeSourceIconType => incomeSourceIconType;
    [JsonIgnore] public const string TitleProperty = nameof(incomeSourceIconType);
}

public enum IncomeSourceIconType
{
    Salary, 
    Savings, 
    Deposits, 
    Coins, 
    Wallet, 
    Gold, 
    Card, 
    Bitcoin, 
    Gift
}
