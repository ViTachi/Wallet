using System;

public static class PremiumMode
{
    public static event Action<Mode> OnModeChanged;

    private static Mode currentMode = Mode.None;
    public static Mode CurrentMode => currentMode;

    public enum Mode
    {
        None, Default, Premium
    }

    public static void SetMode(Mode mode)
    {
        if (currentMode == mode) return;

        currentMode = mode;
        OnModeChanged?.Invoke(currentMode);
    }
}
