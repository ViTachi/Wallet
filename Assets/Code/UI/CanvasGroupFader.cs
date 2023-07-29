using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    private CanvasGroup canvasGroup = null;
    private CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null) 
                canvasGroup = GetComponent<CanvasGroup>(); 
            return canvasGroup;
        }
    }

    public void Show(bool immediately = false, float delay = 0f, float time = 0.3f, Action onComplete = null)
    {
        DOTween.Kill(this, true);

        CanvasGroup.gameObject.SetActive(true);

        if (immediately)
        {
            CanvasGroup.alpha = 1;          
        }
        else
        {
            CanvasGroup.DOFade(1f, time).SetDelay(delay)
                .SetUpdate(true).SetId(this)
                .OnComplete(() => onComplete?.Invoke());
        }
    }

    public void Hide(bool immediately = false, float delay = 0f, float time = 0.3f, Action onComplete = null)
    {
        DOTween.Kill(this, true);
        if (immediately)
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.gameObject.SetActive(false);
        }
        else
        {
            CanvasGroup.DOFade(0f, time).SetDelay(delay)
                .SetUpdate(true).SetId(this)
                .OnComplete(() =>
                {
                    CanvasGroup.gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }
    }
}
