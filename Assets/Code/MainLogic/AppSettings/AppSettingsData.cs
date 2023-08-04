using Newtonsoft.Json;
using System.Collections.Generic;
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


    [JsonIgnore, SerializeField] private List<AccountIcon> accountIcons;
    [JsonIgnore] public IReadOnlyList<AccountIcon> AccountIcons => accountIcons;
    public AccountIcon GetAccountIcon(AccountIconType iconType) => accountIcons.Find(x => x.AccountIconType == iconType);


    [JsonProperty, SerializeField] private CategoriesSettings categoriesSettings;
    [JsonIgnore] public CategoriesSettings CategoriesSettings => categoriesSettings;


    [JsonIgnore, SerializeField] private List<CategoryIcon> categoryIcons;
    [JsonIgnore] public IReadOnlyList<CategoryIcon> CategoryIcons => categoryIcons;
    public CategoryIcon GetCategoryIcon(CategoryType iconType) => categoryIcons.Find(x => x.CategoryIconType == iconType);


    [JsonProperty, SerializeField] private IncomeSourceSettings incomeSourceSettings;
    [JsonIgnore] public IncomeSourceSettings IncomeSourceSettings => incomeSourceSettings;

    
}
