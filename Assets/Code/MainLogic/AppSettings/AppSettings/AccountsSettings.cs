using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class AccountsSettings
{
    [JsonProperty, SerializeField] private List<Account> accounts;
    [JsonProperty, SerializeField] private string selectedAccountIdByDefault;
    [JsonProperty, SerializeField, ArrayElements(IconColor.TitleProp)] private List<IconColor> iconColors;
    
    [JsonIgnore] public IReadOnlyList<Account> Accounts => accounts;
    [JsonIgnore] public IReadOnlyList<IconColor> IconColors => iconColors;
    
    [JsonIgnore] public string SelectedAccountIdByDefault => selectedAccountIdByDefault;
    public IconColor GetIconColor(IconColorType colorType) => iconColors.Find(x => x.IconColorType == colorType);
}
