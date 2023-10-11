using UnityEngine;

public class IncomeChooseButton : MonoBehaviour
{
    [SerializeField] private IconView iconView;
    [SerializeField] private TMPro.TMP_Text nameText;
    [SerializeField] private ScalableButton button;

    private void Awake()
    {
        UpdateView();
        SaveData.Instance.IncomeSourceData.OnSelectIncomeSource += UpdateView;
        button.OnClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UIScreens.Instance.ShowElement<SelectIncomeSourcePanel>();
    }

    private void UpdateView()
    {
        IncomeSource incomeSource = SaveData.Instance.IncomeSourceData.SelectedIncomeSource;
        if (incomeSource == null) return;

        IncomeSourceIcon incomeSourceIcon = AppSettingsData.Instance.IncomeSourceSettings.GetIncomeSourceIcon(incomeSource.Icon);
        if (incomeSourceIcon != null)
            iconView.SetIcon(incomeSourceIcon.Icon);
        IconColor iconColor = AppSettingsData.Instance.GetIconColor(incomeSource.IconColorType);
        if (iconColor != null)
            iconView.SetColor(iconColor.Color);
        nameText.text = incomeSource.Name;
    }
}
