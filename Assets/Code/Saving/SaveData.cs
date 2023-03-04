using System.Collections.Generic;
using Newtonsoft.Json;

public class SaveData
{
    [JsonProperty] public List<OperationData> Operations { get; set; } = new List<OperationData>();
}
