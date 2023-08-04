using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button emptyAreaButton;
    [SerializeField] private RectTransform panelRT;
    [SerializeField] private CanvasGroup fadeGroup;

    public event System.Action OnClose;

    private void Awake()
    {
        emptyAreaButton.onClick.AddListener(() => Hide(false));
    }

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this);
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
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
        OnClose?.Invoke();
        DOTween.Kill(this);
        canvasGroup.interactable = false;

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
