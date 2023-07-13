using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class IncomeSourceSaveData
{
    [JsonProperty] private List<IncomeSource> incomeSources = new List<IncomeSource>();
    [JsonIgnore] public IReadOnlyList<IncomeSource> IncomeSources => incomeSources;
    public void AddIncomeSource(IncomeSource incomeSource)
    {
        if (!IncomeSources.Any(x => x.Id == incomeSource.Id))
        {
            incomeSources.Add(incomeSource);
            SaveData.Save();
        }

    }
    public void RemoveIncomeSource(IncomeSource incomeSource)
    {
        if (IncomeSources.Contains(incomeSource))
        {
            incomeSources.Remove(incomeSource);
            SaveData.Save();
        }
    }
}
