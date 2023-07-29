using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class IconData
{
    [JsonIgnore, SerializeField] private Sprite icon;
    [JsonIgnore] public Sprite Icon => icon;
}