using UnityEngine;
using UnityEngine.UI;

public class PieChartPage : MonoBehaviour
{
    [SerializeField] private MonthSelector monthSelector;
    [SerializeField] private ModeSlider modeSlider;

    [Header("Pie")]
    [SerializeField] private RectTransform expenseModeText;
    [SerializeField] private RectTransform incomeModeText;
    [SerializeField] private TMPro.TMP_Text amountText;
    [SerializeField] private RectTransform circleBackground;
    [SerializeField] private RectTransform piePartsParent;
    [SerializeField] private Image piePartPrefab;

    [Header("Categories")]
    [SerializeField] private RectTransform categoriesParent;
    [SerializeField] private OperationsTypeStatsView operationsTypeStatsViewPrefab;

    SaveData saveData = null;

    private void OnEnable()
    {
        saveData = SaveData.Instance;
        if(saveData != null)
            saveData.AccountsData.OnSelectAccount += OnSelectAccount;

        UpdatePage();
    }

    private void OnDisable()
    {
        if (saveData != null)
            saveData.AccountsData.OnSelectAccount += OnSelectAccount;
    }


    private void OnSelectAccount()
    {

    }

    private void UpdatePage()
    {
        modeSlider.SetMode(1, true);
        UpdateCharts();
    }

    private void UpdateCharts()
    {

    }
}
