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

    public void Initialize(AccountIconType accountIconType, int iconColorIndex = -1)
    {
        AccountIcon accountIcon = AppSettingsData.Instance.AccountsSettings.GetAccountIcon(accountIconType);
        if (accountIcon != null)
            iconView.SetIcon(accountIcon.Icon);
        if(iconColorIndex != -1)
        {
            Color color = AppSettingsData.Instance.AccountsSettings.GetIconColor(iconColorIndex);
            iconView.SetColor(color);
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
