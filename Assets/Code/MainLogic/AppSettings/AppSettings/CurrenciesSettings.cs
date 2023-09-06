using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[Serializable]
public class CurrenciesSettings
{
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private CurrencyType defaultSelectedCurrency;
    [JsonProperty, SerializeField, ArrayElements(Currency.TitleProperty)]
    private List<Currency> currencies;

    [JsonIgnore] public CurrencyType DefaultSelectedCurrency => defaultSelectedCurrency;
    [JsonIgnore] public IReadOnlyList<Currency> Currencies => currencies;
    public Currency GetCurrency(CurrencyType currencyType) => currencies.Find(x => x.CurrencyType == currencyType);
}
