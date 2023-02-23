using UnityEngine;

public class ShowInDebugMode : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(DebugMode.IsDebugMode);
        DebugMode.OnModeChanged += (enabled) => gameObject.SetActive(enabled);
    }
}
