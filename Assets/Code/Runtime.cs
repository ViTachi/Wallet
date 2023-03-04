using UnityEngine;

public class Runtime : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        DataSaver.Load("SaveData");
        PremiumMode.SetMode(PremiumMode.Mode.Default);
    }
}
