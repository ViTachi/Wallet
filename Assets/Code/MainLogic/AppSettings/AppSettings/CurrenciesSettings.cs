using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class CurrenciesSettings
{
    [JsonProperty, SerializeField] private CurrencyType defaultSelectedCurrency;
    [JsonProperty, SerializeField] private List<Currency> currencies;

    [JsonIgnore] public CurrencyType DefaultSelectedCurrency => defaultSelectedCurrency;
    [JsonIgnore] public IReadOnlyList<Currency> Currencies => currencies;
    public Currency GetCurrency(CurrencyType currencyType) => currencies.Find(x => x.CurrencyType == currencyType);
}
