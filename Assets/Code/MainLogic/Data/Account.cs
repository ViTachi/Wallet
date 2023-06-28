using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[System.Serializable]
public class Account
{
    [JsonProperty, SerializeField] private string id;
    [JsonProperty, SerializeField] private string name;
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private AccountIconType icon;
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty, SerializeField] private CurrencyType currency;
    [JsonProperty, SerializeField] private float balance;

    [JsonIgnore] public string Id => id;
    [JsonIgnore] public string Name => name;
    [JsonIgnore] public AccountIconType Icon => icon;
    [JsonIgnore] public CurrencyType Currency => currency;
    [JsonIgnore] public float Balance => balance;

    public Account(string id, string name, AccountIconType icon, CurrencyType currency, float balance = 0f)
    {
        SetId(id);
        SetName(name);
        SetIcon(icon);
        SetCurrency(currency);
        SetBalance(balance);
    }

    public void SetId(string id) => this.id = id;
    public void SetName(string name) => this.name = name;
    public void SetIcon(AccountIconType icon) => this.icon = icon;
    public void SetCurrency(CurrencyType currency) => this.currency = currency;
    public void SetBalance(float balance) => this.balance = balance;  
}