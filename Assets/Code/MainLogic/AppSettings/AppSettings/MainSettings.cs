using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class MainSettings
{
    [JsonProperty, SerializeField] private string themeIdByDefault;
}
