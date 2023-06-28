using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AppSettings))]
public class AppSettingsEditor : Editor
{
    [SerializeField] private TextAsset jsonFile;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AppSettings appSettings = (AppSettings)target;
        if (GUILayout.Button("Load From Json"))
        {
            appSettings.LoadDataFromJson();
        }

        if (GUILayout.Button("Save To Json"))
        {
            appSettings.SaveToJson();
        }
    }
}