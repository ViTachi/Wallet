using DG.Tweening;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelRT;
    [SerializeField] private CanvasGroup fadeGroup;

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this);
        gameObject.SetActive(true);
        if (immediately)
        {
            panelRT.anchoredPosition = new Vector2(panelRT.anchoredPosition.x, 0);
            fadeGroup.alpha = 1;
        }
        else
        {
            panelRT.anchoredPosition = new Vector2(panelRT.anchoredPosition.x, -panelRT.rect.height);
            panelRT.DOAnchorPosY(0, 0.4f)
                .SetUpdate(true)
                .SetId(this)
                .SetEase(Ease.OutBack);

            fadeGroup.alpha = 0;
            fadeGroup.DOFade(1, 0.4f)
                .SetUpdate(true)
                .SetId(this);
        }
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
            panelRT.DOAnchorPosY(-panelRT.rect.height, 0.4f)
                .SetUpdate(true)
                .SetId(this)
                .SetEase(Ease.InBack)
                .OnComplete(() => gameObject.SetActive(false));
            fadeGroup.DOFade(0, 0.4f)
                .SetUpdate(true)
                .SetId(this);
        }
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
