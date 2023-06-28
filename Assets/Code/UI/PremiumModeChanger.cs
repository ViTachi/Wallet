using UnityEngine;

public class PremiumModeChanger : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private ScalableButton button;

    private void Awake()
    {
        button.OnClick.AddListener(ChangeMode);
    }

    private void ChangeMode()
    {
        PremiumMode.SetMode(PremiumMode.CurrentMode == PremiumMode.Mode.Default
        ? PremiumMode.Mode.Premium : PremiumMode.Mode.Default);
        text.text = $"Mode : {PremiumMode.CurrentMode}";
    }
}
