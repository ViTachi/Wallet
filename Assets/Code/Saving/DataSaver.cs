using System.IO;
using Newtonsoft.Json;

public static class DataSaver
{
    public static SaveData SaveData { get; private set; }

    private static string saveName;

    public static void Load(string saveName)
    {
        DataSaver.saveName = saveName;
        string json = FilesManager.LoadTextFromFile(Path.Combine("Saves", $"{saveName}.js"));
        if (string.IsNullOrEmpty(json))
        {
            SaveData = new SaveData();
        }
        else
        {
            SaveData = JsonConvert.DeserializeObject<SaveData>(json);
        }
    }
    // public static void Load(string saveName)
    // {
    //     saveData = FilesManager.LoadDataFromFile<Data>(Path.Combine("Saves", saveName));
    //     if (saveData == null) saveData = new Data();
    // }

    public static void Save()
    {
        FilesManager.SaveTextToFile(Path.Combine("Saves", $"{saveName}.js"), JsonConvert.SerializeObject(SaveData, Formatting.Indented));
    }
    // public static void Save(string saveName)
    // {
    //     FilesManager.SaveDataToFile(Path.Combine("Saves", saveName), saveData);
    // }

    public static void Delete(string saveName)
    {
        FilesManager.DeleteFile(Path.Combine("Saves", saveName));
    }
}
