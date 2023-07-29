using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Currency 
{
    [JsonProperty, SerializeField] private CurrencyType currencyType;
    [JsonProperty, SerializeField] private string currencyName;
    [JsonProperty, SerializeField] private string currencyFullName;
    [JsonProperty, SerializeField] private string iconText;

    [JsonIgnore, SerializeField] private Sprite icon;

    public CurrencyType CurrencyType => currencyType;
    public string CurrencyName => currencyName;
    public string CurrencyFullName => currencyFullName;
    public Sprite Icon => icon;
    public string IconText => iconText;
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
