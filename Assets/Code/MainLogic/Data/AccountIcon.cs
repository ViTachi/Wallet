using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class AccountIcon : IconData
{
    [JsonProperty, SerializeField] private AccountIconType accountIconType;
    [JsonIgnore] public AccountIconType AccountIconType => accountIconType;
}

public enum AccountIconType
{
    None,
    Wallet,
    Card,
    Card2,
    Card3,
    Card4,
    Money,
    MoneyBag,
    Banknote,
    Mastercard,
    Visa,
    PayPal,
    V,
    Apple,
    Amex,
    Bitcoin,
    WebMoney,
    Io,
}
