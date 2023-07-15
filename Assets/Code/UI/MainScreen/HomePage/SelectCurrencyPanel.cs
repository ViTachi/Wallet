using System;
using DG.Tweening;
using UnityEngine;

public class SelectCurrencyPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelRT;
    [SerializeField] private ScalableButton closeButton;
    [SerializeField] private CanvasGroup headerGroup;

    [SerializeField] private RectTransform currenciesParent;
    [SerializeField] private ScalableButton searchButton;

    public event Action OnClose;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        searchButton.OnClick.AddListener(OnSearchClicked);
    }

    private void OnSearchClicked()
    {
        
    }

    private void OnChooseCurrency()
    {

    }

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this, true);
        headerGroup.gameObject.SetActive(true);
        panelRT.gameObject.SetActive(true);
        if (immediately)
        {
            headerGroup.alpha = 1;
        }
        else
        {
            headerGroup.alpha = 0;
            headerGroup.DOFade(1f, 0.3f).SetDelay(0.15f).SetUpdate(true).SetId(this);
            panelRT.anchoredPosition = new Vector2(panelRT.rect.width,
                panelRT.anchoredPosition.y);
            panelRT.DOAnchorPosX(0, 0.3f).SetUpdate(true).SetId(this);
        }
    }

    public void Hide(bool immediately = false)
    {
        DOTween.Kill(this, true);
        if (immediately)
        {
            headerGroup.alpha = 0;
            headerGroup.gameObject.SetActive(false);
            panelRT.gameObject.SetActive(false);
        }
        else
        {
            headerGroup.DOFade(0f, 0.3f)
                .SetUpdate(true).SetId(this)
                .OnComplete(() => headerGroup.gameObject.SetActive(false));
            panelRT.DOAnchorPosX(panelRT.rect.width, 0.3f).SetUpdate(true).SetId(this)
                .OnComplete(() => panelRT.gameObject.SetActive(false));
        }
        OnClose?.Invoke();
    }
}

