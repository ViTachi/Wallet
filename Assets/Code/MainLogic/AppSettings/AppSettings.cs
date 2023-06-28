using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AppSettings", fileName = "AppSettings")]
public class AppSettings : ScriptableObject
{
    [SerializeField] private TextAsset appSettingsJson;

    [Space(20)]
    [SerializeField] private AppSettingsData appSettingsData;
    public AppSettingsData AppSettingsData => appSettingsData;

    public void LoadDataFromJson()
    {
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
        jsonSerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
        JsonConvert.PopulateObject(appSettingsJson.text, appSettingsData, jsonSerializerSettings);
    }
    
    public void SaveToJson()
    {
        FilesManager.ConvertStringToTextAsset(JsonConvert.SerializeObject(appSettingsData, Formatting.Indented), "/Data/AppSettings/AppSettings");
    }
}