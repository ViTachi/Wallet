using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class AccountsSettings
{
    [JsonProperty, SerializeField] private List<Account> accounts;
    [JsonProperty, SerializeField] private string selectedAccountIdByDefault;
    [JsonProperty, SerializeField] private List<Color> iconColors;
    [JsonProperty, SerializeField] private List<AccountIcon> accountIcons;

    [JsonIgnore] public IReadOnlyList<Account> Accounts => accounts;
    [JsonIgnore] public IReadOnlyList<Color> IconColors => iconColors;
    [JsonIgnore] public IReadOnlyList<AccountIcon> AccountIcons => accountIcons;
    [JsonIgnore] public string SelectedAccountIdByDefault => selectedAccountIdByDefault;

    public Color GetIconColor(int index) => iconColors.Count > 0 ? iconColors[Mathf.Clamp(index, 0, iconColors.Count - 1)] : default;
    public AccountIcon GetAccountIcon(AccountIconType iconType) => accountIcons.Find(x => x.AccountIconType == iconType);
}
