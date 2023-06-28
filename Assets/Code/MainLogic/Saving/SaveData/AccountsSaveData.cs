using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class AccountsSaveData
{
    [JsonProperty] private List<Account> accounts = new List<Account>();
    [JsonProperty] private string selectedAccountId = null;

    public event System.Action OnSelectAccount;
    [JsonIgnore] public Account SelectedAccount => GetAccount(selectedAccountId);

    [JsonIgnore]
    public IReadOnlyList<Account> Accounts
    {
        get
        {
            if (accounts.Count == 0)
            {
                foreach (var account in AppSettingsData.Instance.AccountsSettings.Accounts)
                {
                    accounts.Add(account);
                }
            }
            return accounts;
        }
    }

    [JsonIgnore]
    public string SelectedAccountId
    {
        get
        {
            if (string.IsNullOrEmpty(selectedAccountId))
                selectedAccountId = AppSettingsData.Instance.AccountsSettings.SelectedAccountIdByDefault;
            return selectedAccountId;
        }
        set
        {
            selectedAccountId = value;
            OnSelectAccount?.Invoke();
            SaveData.Save();
        }
    }

    public void AddAccount(Account account)
    {
        if (!accounts.Any(x => x.Id == account.Id))
            accounts.Add(account);
    }
    public void RemoveAccount(Account account) => accounts.Remove(account);
    public Account GetAccount(string id) => accounts.Find(x => x.Id == id);
}
