using System;
using Newtonsoft.Json;
using UnityEngine;

public class OperationData : MonoBehaviour
{
    [JsonProperty] public float Value { get; set; }
    [JsonProperty] public string CategoryId { get; set; }
    [JsonProperty] public DateTime Date { get; set; }
    [JsonProperty] public string Note { get; set; }
}
