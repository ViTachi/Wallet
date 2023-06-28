using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class AccountsSettings
{
    [JsonProperty, SerializeField] private List<Account> accounts;
    [JsonProperty, SerializeField] private string selectedAccountIdByDefault;
    [JsonIgnore] public IReadOnlyList<Account> Accounts => accounts;
    [JsonIgnore] public string SelectedAccountIdByDefault => selectedAccountIdByDefault;


    [JsonIgnore, SerializeField] private List<AccountIcon> accountIcons;
    public AccountIcon GetAccountIcon(AccountIconType iconType) => accountIcons.Find(x => x.AccountIconType == iconType);
}
