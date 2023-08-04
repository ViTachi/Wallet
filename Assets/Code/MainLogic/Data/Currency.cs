using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[System.Serializable]
public class Currency 
{
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private CurrencyType currencyType;
    [JsonProperty, SerializeField] private string currencyName;
    [JsonProperty, SerializeField] private string currencyFullName;
    [JsonProperty, SerializeField] private string iconText;

    [JsonIgnore] public CurrencyType CurrencyType => currencyType;
    [JsonIgnore] public string CurrencyName => currencyName;
    [JsonIgnore] public string CurrencyFullName => currencyFullName;
    [JsonIgnore] public string IconText => iconText;
}

public enum CurrencyType
{
    None,
    USD,
    RUB,
    BYR,
    EUR,
    AFN,
    ARS,
    AMD,
    AUD,
    BAM,
    BHD,
    BRL,
}
