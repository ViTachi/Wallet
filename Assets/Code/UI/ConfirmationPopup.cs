using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopup : MonoBehaviour, IScreenElement
{
    [SerializeField] private CanvasGroup shade;
    [SerializeField] private CanvasGroup popup;
    [SerializeField] private TMPro.TMP_Text titleText;
    [SerializeField] private TMPro.TMP_Text descriptionText;
    [SerializeField] private RectTransform deleteTextRT;
    [SerializeField] private RectTransform discardTextRT;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button acceptButton;

    public enum Variant { Delete, Discard }

    private Action<bool> callback;

    private void Awake()
    {
        cancelButton.onClick.AddListener(OnCancel);
        acceptButton.onClick.AddListener(OnAccept);
    }

    public void SetVariant(Variant variant)
    {
        deleteTextRT.gameObject.SetActive(false);
        discardTextRT.gameObject.SetActive(false);

        switch (variant)
        {
            case Variant.Delete:
                deleteTextRT.gameObject.SetActive(true);
                break;
            case Variant.Discard:
                discardTextRT.gameObject.SetActive(true);
                break;
        }
    }

    public void Initialize(Action<bool> callback, string title, Variant variant = Variant.Delete, string description = null)
    {
        this.callback = callback;
        titleText.text = title;
        SetVariant(variant);
        bool hasDescription = !string.IsNullOrEmpty(description);
        descriptionText.gameObject.SetActive(hasDescription);
        if (hasDescription)
        {
            descriptionText.text = description;
        }
    }
    
    private void OnCancel()
    {
        callback?.Invoke(false);
        Hide();
    }

    private void OnAccept()
    {
        callback?.Invoke(true);
        Hide();
    } 

    public void Hide(bool immediately = false)
    {
        DOTween.Kill(this);

        if (immediately)
        {
            gameObject.SetActive(false);
        }
        else
        {
            shade.DOFade(0, 0.15f)
                .SetUpdate(true)
                .SetId(this);
            popup.transform.DOScale(0, 0.15f)
                //.SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetId(this)
                .OnComplete(() => gameObject.SetActive(false));
        }

        callback = null;
    }

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this);

        gameObject.SetActive(true);

        if (immediately)
        {
            shade.alpha = 1;
            popup.transform.localScale = Vector3.one;
        }
        else
        {
            shade.alpha = 0;
            popup.transform.localScale = Vector3.zero;

            shade.DOFade(1, 0.3f)
                .SetUpdate(true)
                .SetId(this);
            popup.transform.DOScale(1, 0.3f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
                .SetId(this);
        }
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
