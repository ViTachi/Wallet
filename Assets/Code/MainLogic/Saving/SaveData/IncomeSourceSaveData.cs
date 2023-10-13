using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class IncomeSourceSaveData
{
    [JsonProperty] private List<IncomeSource> incomeSources = new List<IncomeSource>();
    [JsonProperty] private List<IncomeSource> permanentIncomeSources = new List<IncomeSource>();
    [JsonProperty] private string selectedIncomeSourceId = null;

    public event System.Action OnIncomeSourcesChanged;
    public event System.Action OnSelectIncomeSource;
    [JsonIgnore] public IncomeSource SelectedIncomeSource => GetIncomeSource(SelectedIncomeSourceId);

    [JsonIgnore] public IReadOnlyList<IncomeSource> IncomeSources
    {
        get
        {
            if (incomeSources.Count == 0)
            {
                foreach (var incomeSource in AppSettingsData.Instance.IncomeSourceSettings.IncomeSources)
                {
                    incomeSources.Add((IncomeSource)incomeSource.Clone());
                    permanentIncomeSources.Add((IncomeSource)incomeSource.Clone());
                }
            }
            return incomeSources;
        }
    }

    [JsonIgnore] public IReadOnlyList<IncomeSource> PermanentIncomeSources => permanentIncomeSources;

    [JsonIgnore]
    public string SelectedIncomeSourceId
    {
        get
        {
            if (string.IsNullOrEmpty(selectedIncomeSourceId))
                selectedIncomeSourceId = AppSettingsData.Instance.IncomeSourceSettings.SelectedIncomeSourceIdByDefault;
            return selectedIncomeSourceId;
        }
        set
        {
            selectedIncomeSourceId = value;
            OnSelectIncomeSource?.Invoke();
            SaveData.Save();
        }
    }

    public IncomeSource GetIncomeSource(string id) => IncomeSources.FirstOrDefault(x => x.Id == id);
    public IncomeSource GetPermanentIncomeSource(string id) => PermanentIncomeSources.FirstOrDefault(x => x.Id == id);

    public void AddIncomeSource(IncomeSource incomeSource)
    {
        if (!IncomeSources.Any(x => x.Id == incomeSource.Id))
        {
            incomeSources.Add((IncomeSource)incomeSource.Clone());
            permanentIncomeSources.Add((IncomeSource)incomeSource.Clone());
            OnIncomeSourcesChanged?.Invoke();
            SaveData.Save();
        }

    }
    public void RemoveIncomeSource(string incomeSourceId)
    {
        if (IncomeSources.Where(x => x.Id == incomeSourceId).Count() > 0)
        {
            incomeSources.RemoveAll(x => x.Id == incomeSourceId);
            if (SelectedIncomeSourceId == incomeSourceId)
                SelectedIncomeSourceId = IncomeSources.First()?.Id ?? null;
            OnIncomeSourcesChanged?.Invoke();
            SaveData.Save();
        }
    }
}
