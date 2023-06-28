using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class IncomeSourceSaveData
{
    [JsonProperty] private List<IncomeSource> incomeSources = new List<IncomeSource>();
    [JsonIgnore] public IReadOnlyList<IncomeSource> IncomeSources => incomeSources;
    public void AddIncomeSource(IncomeSource incomeSource)
    {
        if (!incomeSources.Any(x => x.Id == incomeSource.Id))
            incomeSources.Add(incomeSource);
    }
    public void RemoveIncomeSource(IncomeSource incomeSource) => incomeSources.Remove(incomeSource);
}
