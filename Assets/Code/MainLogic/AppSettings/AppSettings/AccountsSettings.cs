using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class AccountsSettings
{
    [JsonProperty, SerializeField] private List<Account> accounts;
    [JsonProperty, SerializeField] private string selectedAccountIdByDefault;
    [JsonIgnore, SerializeField, ArrayElements(AccountIcon.TitleProperty)]
    private List<AccountIcon> accountIcons;

    [JsonIgnore] public IReadOnlyList<Account> Accounts => accounts;
    [JsonIgnore] public IReadOnlyList<AccountIcon> AccountIcons => accountIcons;

    [JsonIgnore] public string SelectedAccountIdByDefault => selectedAccountIdByDefault;
    public AccountIcon GetAccountIcon(AccountIconType iconType) => accountIcons.Find(x => x.AccountIconType == iconType);
}
