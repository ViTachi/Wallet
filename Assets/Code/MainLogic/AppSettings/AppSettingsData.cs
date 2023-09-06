using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class AppSettingsData
{
    public static AppSettingsData Instance => AppData.Instance.AppSettings.AppSettingsData;


    [JsonProperty, SerializeField] private MainSettings mainSettings;
    [JsonIgnore] public MainSettings MainSettings => mainSettings;


    [JsonProperty, SerializeField] private CurrenciesSettings currenciesSettings;
    [JsonIgnore] public CurrenciesSettings CurrenciesSettings => currenciesSettings;


    [JsonProperty, SerializeField] private AccountsSettings accountsSettings;
    [JsonIgnore] public AccountsSettings AccountsSettings => accountsSettings;


    [JsonProperty, SerializeField] private CategoriesSettings categoriesSettings;
    [JsonIgnore] public CategoriesSettings CategoriesSettings => categoriesSettings;


    [JsonProperty, SerializeField] private IncomeSourceSettings incomeSourceSettings;
    [JsonIgnore] public IncomeSourceSettings IncomeSourceSettings => incomeSourceSettings;

    
}
