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


    [JsonProperty, SerializeField] private CategoriesSettings categoriesSettings;
    [JsonIgnore] public CategoriesSettings CategoriesSettings => categoriesSettings;


    [JsonProperty, SerializeField] private IncomeSourceSettings incomeSourceSettings;
    [JsonIgnore] public IncomeSourceSettings IncomeSourceSettings => incomeSourceSettings;

    #region IconColors

    [JsonProperty, SerializeField, ArrayElements(IconColor.TitleProperty)]
    private List<IconColor> iconColors;

    [JsonIgnore] public IReadOnlyList<IconColor> IconColors => iconColors;

    public IconColor GetIconColor(IconColorType colorType) => iconColors.Find(x => x.IconColorType == colorType);

    #endregion
}
