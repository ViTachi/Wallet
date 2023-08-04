using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class MainSettings
{
    [JsonProperty, SerializeField] private string themeIdByDefault;
    [JsonIgnore, SerializeField] private int i = 3;
}
