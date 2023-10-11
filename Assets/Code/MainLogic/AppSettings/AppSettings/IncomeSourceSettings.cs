using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class IncomeSourceSettings
{
    [JsonProperty, SerializeField] private List<IncomeSource> incomeSources;
    [JsonProperty, SerializeField] private string selectedIncomeSourceIdByDefault;
    [JsonProperty, SerializeField, ArrayElements(IncomeSourceIcon.TitleProperty)] 
    private List<IncomeSourceIcon> incomeSourceIcons;

    [JsonIgnore] public IReadOnlyList<IncomeSourceIcon> IncomeSourceIcons => incomeSourceIcons;
    [JsonIgnore] public IReadOnlyList<IncomeSource> IncomeSources => incomeSources;

    [JsonIgnore] public string SelectedIncomeSourceIdByDefault => selectedIncomeSourceIdByDefault;

    public IncomeSourceIcon GetIncomeSourceIcon(IncomeSourceIconType iconType) =>
        incomeSourceIcons.Find(x => x.IncomeSourceIconType == iconType);
}
