using System;
using UnityEngine;

public class DebugMode : MonoBehaviour
{
    private static bool isDebugMode =false;
    public static bool IsDebugMode => isDebugMode;

    public static event Action<bool> OnModeChanged;

    private static float lastTap = 0;

    private void Awake()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        foreach (var debugObject in FindObjectsOfType<ShowInDebugMode>(true))
        {
            debugObject.gameObject.SetActive(true);
        }
#else
        gameObject.SetActive(false);
#endif
    }

    public static void SetDebugModeEnabled(bool enabled)
    {
        isDebugMode = enabled;
        OnModeChanged?.Invoke(IsDebugMode);
    }

    private void Update()
    {
        if (1f > Time.time - lastTap) return;
        if (Input.touchCount > 2 || Input.GetKey(KeyCode.C))
        {
            SetDebugModeEnabled(!IsDebugMode);

            lastTap = Time.time;
        }
    }

}
