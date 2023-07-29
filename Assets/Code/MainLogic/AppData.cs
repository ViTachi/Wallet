using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class AppData : Singleton<AppData>
{
    [SerializeField] private AppSettings appSettings;
    [SerializeField] private string saveName = "SaveData";
    [SerializeField] private string savesFolderName = "Saves";
    public AppSettings AppSettings => appSettings;

    private SaveData saveData = null;
    public SaveData SaveData
    {
        get
        {
            if(saveData == null) Load();
            return saveData;
        }
    }

    public void Load()
    {
        string json = FilesManager.LoadTextFromFile(Path.Combine(savesFolderName, $"{saveName}.js"));
        if (string.IsNullOrEmpty(json))
        {
            saveData = new SaveData();
        }
        else
        {
            saveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
    }

    public void Save()
    {
        FilesManager.SaveTextToFile(Path.Combine(savesFolderName, $"{saveName}.js"), JsonConvert.SerializeObject(saveData, Formatting.Indented));
    }

    public void Delete()
    {
        FilesManager.DeleteFile(Path.Combine(savesFolderName, $"{saveName}.js"));
    }

    protected override void Setup()
    {
        
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AppData))]
    public class AppDataEditor : UnityEditor.Editor
    {
        public AppData AppData => target as AppData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Delete Save"))
            {
                AppData.Delete();
                Debug.Log("Save deleted");
            }
        }
    }
#endif
}
