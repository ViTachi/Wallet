using System;
using UnityEngine;
using UnityEngine.UI;

public class IncomeSourceView : MonoBehaviour, ISelectable<IncomeSourceView>
{
    [SerializeField] private ScalableButton button;
    [SerializeField] private IconView iconView;
    [SerializeField] private Graphic background;
    [SerializeField] private TMPro.TMP_Text sourceName;
    [SerializeField] private GameObject removeIcon;

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
            iconView.SetIcon(icon.Icon);

        sourceName.text = incomeSource.Name;
        IconColor iconColor = AppSettingsData.Instance.GetIconColor(incomeSource.IconColorType);
        if (iconColor != null)
        {
            Color backgroundColor = iconColor.Color;
            backgroundColor.a = 0.05f;
            background.color = backgroundColor;
            iconView.SetColor(iconColor.Color);
        }
    }

    public void SetRemoveButtonActive(bool active) => removeIcon.gameObject.SetActive(active);

    public void SetIsSelected(bool selected)
    {

    }
}
