using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class AccountsSaveData
{
    [JsonProperty] private List<Account> accounts = new List<Account>();
    [JsonProperty] private string selectedAccountId = null;

    public event System.Action OnAccountsChanged;
    public event System.Action OnSelectAccount;
    [JsonIgnore] public Account SelectedAccount => GetAccount(SelectedAccountId);

    [JsonIgnore]
    public IReadOnlyList<Account> Accounts
    {
        get
        {
            if (accounts.Count == 0)
            {
                foreach (var account in AppSettingsData.Instance.AccountsSettings.Accounts)
                {
                    accounts.Add((Account)account.Clone());
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
        if (!Accounts.Any(x => x.Id == account.Id))
        {
            accounts.Add((Account)account.Clone());
            OnAccountsChanged?.Invoke();
            SaveData.Save();
        }    
    }
    public void RemoveAccount(Account account)
    {
        if (Accounts.Contains(account))
        {
            accounts.Remove(account);
            OnAccountsChanged?.Invoke();
            SaveData.Save();
        }
    }
    public Account GetAccount(string id) => Accounts.Where(x => x.Id == id).FirstOrDefault();
}
