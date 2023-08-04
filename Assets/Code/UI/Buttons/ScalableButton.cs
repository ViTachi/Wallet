using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScalableButton : Selectable, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent onClick;

    [SerializeField] private float scaleOnPressed = 0.95f;
    [SerializeField] private float changeColorDuration = 0.4f;

    [SerializeField] private List<StateSetting> states;

    public UnityEvent OnClick => onClick;

    [System.Serializable]
    public class StateSetting
    {
        public State state;
        public Graphic graphic;
        public Color color = Color.white;

        public void SetupColor(MonoBehaviour tweenId, float duration = 0)
        {
            if (duration == 0) graphic.color = color;
            else
            {
                graphic.DOColor(color, duration)
                    .SetEase(Ease.OutSine)
                    .SetId(tweenId)
                    .SetUpdate(true);
            }
        }
    } 

    public enum State { None, Disabled, Active, Selected }

    private State currentState = State.None;
    public State CurrentState => currentState;

    public void SetState(State state, bool immediately = false)
    {
        if(currentState != state)
        {
            currentState = state;

            foreach (var stateSetting in states.Where(x => x.state == state))
            {
                stateSetting.SetupColor(this, immediately ? 0f : changeColorDuration);
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (!IsInteractable()) return;

        DOTween.Kill(this);
        transform.DOScale(Vector3.one * scaleOnPressed, 0.7f)
            .SetEase(Ease.OutSine)
            .SetSpeedBased()
            .SetId(this)
            .SetUpdate(true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (!IsInteractable()) return;

        DOTween.Kill(this);
        transform.DOScale(Vector3.one, 0.7f)
            .SetEase(Ease.OutSine)
            .SetSpeedBased()
            .SetId(this)
            .SetUpdate(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        DOTween.Kill(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsInteractable()) return;

        onClick?.Invoke();
    }
}
