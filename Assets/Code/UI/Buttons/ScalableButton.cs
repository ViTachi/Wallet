using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScalableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent onClick;

    [SerializeField] private bool interactable = true;
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

    public void SetState(State state)
    {
        if(currentState != state)
        {
            currentState = state;

            foreach (var stateSetting in states.Where(x => x.state == state))
            {
                stateSetting.SetupColor(this, changeColorDuration);
            }
        }
    }

    public void SetIsInteractable(bool interactable)
    {
        this.interactable = interactable;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;

        DOTween.Kill(this);
        transform.DOScale(Vector3.one * scaleOnPressed, 0.7f)
            .SetEase(Ease.OutSine)
            .SetSpeedBased()
            .SetId(this)
            .SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) return;

        DOTween.Kill(this);
        transform.DOScale(Vector3.one, 0.7f)
            .SetEase(Ease.OutSine)
            .SetSpeedBased()
            .SetId(this)
            .SetUpdate(true);
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;

        onClick?.Invoke();
    }
}
