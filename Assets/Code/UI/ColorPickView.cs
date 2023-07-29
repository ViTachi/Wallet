using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickView : MonoBehaviour, ISelectable<ColorPickView>
{
    [SerializeField] private ScalableButton button;
    [SerializeField] private RectTransform selector;
    [SerializeField] private Image colorCircle;
    [SerializeField] private Image selectorIcon;

    public bool IsSelected => selector.gameObject.activeSelf;

    public event Action<ColorPickView> OnSelected;

    private void Awake()
    {
        button.OnClick.AddListener(() => OnSelected?.Invoke(this));
    }

    public void Initialize(Color color)
    {
        colorCircle.color = color;
        selectorIcon.color = color;
    }

    public void SetIsSelected(bool selected)
    {
        selector.gameObject.SetActive(selected);
    }
}
