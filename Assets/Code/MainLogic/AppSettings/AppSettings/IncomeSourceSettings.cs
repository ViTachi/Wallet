using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class IncomeSourceSettings
{
    [JsonProperty, SerializeField] private List<IncomeSource> incomeSources;
    [JsonIgnore] public IReadOnlyList<IncomeSource> IncomeSources => incomeSources;

    [JsonIgnore, SerializeField] private List<IncomeSourceIcon> incomeSourceIcons;
}
