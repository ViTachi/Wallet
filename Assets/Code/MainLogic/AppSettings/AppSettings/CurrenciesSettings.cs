using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class CurrenciesSettings
{
    [JsonIgnore, SerializeField] private List<Currency> currencies;
    public Currency GetCurrency(CurrencyType currencyType) => currencies.Find(x => x.CurrencyType == currencyType);
}
