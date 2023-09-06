using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class AccountIcon : IconData
{
    [JsonIgnore, SerializeField] private AccountIconType accountIconType;
    [JsonIgnore] public AccountIconType AccountIconType => accountIconType;
    [JsonIgnore] public const string TitleProperty = nameof(accountIconType);
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
