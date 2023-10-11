using System;
using UnityEngine;
using UnityEngine.UI;

public class IncomeSourceView : MonoBehaviour, ISelectable<IncomeSourceView>
{
    [SerializeField] private ScalableButton button;
    [SerializeField] private Image sourceIcon;
    [SerializeField] private Graphic background;
    [SerializeField] private TMPro.TMP_Text sourceName;

    public bool IsSelected => false;

    public event Action<IncomeSourceView> OnSelected;

    private void Awake()
    {
        button.OnClick.AddListener(() => OnSelected?.Invoke(this));
    }

    public void Initialize(IncomeSource incomeSource)
    {
        IncomeSourceIcon icon = AppSettingsData.Instance.IncomeSourceSettings.GetIncomeSourceIcon(incomeSource.Icon);
        if (icon != null)
            sourceIcon.sprite = icon.Icon;

        sourceName.text = incomeSource.Name;
        IconColor iconColor = AppSettingsData.Instance.GetIconColor(incomeSource.IconColorType);
        if (iconColor != null)
        {
            Color backgroundColor = iconColor.Color;
            backgroundColor.a = 0.05f;
            background.color = backgroundColor;
            sourceIcon.color = iconColor.Color;
        }
    }

    public void SetIsSelected(bool selected)
    {

    }
}
