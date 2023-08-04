using System;
using UnityEngine;

public class AccountIconView : MonoBehaviour, ISelectable<AccountIconView>
{
    [SerializeField] private ScalableButton button;
    [SerializeField] private IconView iconView;
    [SerializeField] private RectTransform selectorIcon;

    public bool IsSelected => selectorIcon.gameObject.activeSelf;

    public event Action<AccountIconView> OnSelected;

    private void Awake()
    {
        button.OnClick.AddListener(() => OnSelected?.Invoke(this));
    }

    public void Initialize(AccountIconType accountIconType, IconColorType iconColorType = IconColorType.None)
    {
        AccountIcon accountIcon = AppSettingsData.Instance.GetAccountIcon(accountIconType);
        if (accountIcon != null)
            iconView.SetIcon(accountIcon.Icon);
        if (iconColorType != IconColorType.None)
        {
            IconColor iconColor = AppSettingsData.Instance.AccountsSettings.GetIconColor(iconColorType);
            if (iconColor != null)
            {
                iconView.SetColor(iconColor.Color);
            }
        }
    }

    public void SetColor(Color color)
    {
        iconView.SetColor(color);
    }

    public void SetIsSelected(bool selected)
    {
        selectorIcon.gameObject.SetActive(selected);
    }
}
