using UnityEngine;

public class KeepFontSize : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private TMPro.TMP_Text referenseText;
    [SerializeField] private float sizeRatio;

    private void OnEnable()
    {
        referenseText.OnPreRenderText += HandleTextSize;
    }

    private void OnDisable()
    {
        referenseText.OnPreRenderText -= HandleTextSize;
    }

    private void HandleTextSize(TMPro.TMP_TextInfo info)
    {
        text.fontSize = referenseText.fontSize * sizeRatio;
        text.ForceMeshUpdate();
    }
}
