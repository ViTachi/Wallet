using DG.Tweening;
using UnityEngine;

public class MessagePopup : MonoBehaviour, IScreenElement
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMPro.TMP_Text messageText;

    private float duration = 1f;

    public enum Type
    {
        Complete,
        Error,
    }

    public void Initialize(string message, float duration = 1f, Type type = Type.Complete)
    {
        messageText.text = message;
        this.duration = duration;
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
            canvasGroup.DOFade(0f, 0.5f).SetUpdate(true).SetId(this).OnComplete(() => gameObject.SetActive(false));
        }
    }

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this);

        gameObject.SetActive(true);

        if (immediately)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0f;
            DOTween.Sequence()
                .Append(canvasGroup.DOFade(1f, 0.5f).SetUpdate(true).SetId(this))
                .AppendInterval(duration)
                .OnComplete(() => Hide(false));
        }
    }
}
