using DG.Tweening;
using UnityEngine;

public class ClearTextButton : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField inputField;
    [SerializeField] private ScalableButton button;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool alwaysShow;
    [SerializeField] private bool showAnimation = true;

    private bool isSelected = false;
    
    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnTextChanged);
        button.OnClick.AddListener(ClearText);
        if (!alwaysShow)
        {
            inputField.onSelect.AddListener(OnInputFieldSelect);
            inputField.onDeselect.AddListener(OnInputFieldDeselect);
        }

        HandleButton();
    }

    private void OnInputFieldSelect(string text)
    {
        isSelected = true;
        HandleButton();     
    }

    private void OnInputFieldDeselect(string text)
    {
        isSelected = false;
        HandleButton();      
    }

    private void OnTextChanged(string text) => HandleButton();

    private void HandleButton()
    {
        bool isTextFieldEmpty = string.IsNullOrEmpty(inputField.text);
        button.interactable = !isTextFieldEmpty;
        canvasGroup.blocksRaycasts = !isTextFieldEmpty;

        SetIsVisible(!isTextFieldEmpty && (isSelected || alwaysShow));
    }

    private void SetIsVisible(bool visible)
    {
        DOTween.Kill(this);
        DOTween.Kill(button);
        if (showAnimation)
        {
            transform.DOScale(visible ? 1f : 0f, visible ? 0.15f : 0.1f)
                .SetEase(visible ? Ease.OutBack : Ease.Unset).SetUpdate(true).SetId(this);
        }
        else
        {
            transform.localScale = visible ? Vector3.one : Vector3.zero;
        }
    }

    private void ClearText()
    {
        inputField.text = string.Empty;
    }
}
