using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonthSelector : MonoBehaviour
{
    [SerializeField] private ScalableButton leftButton;
    [SerializeField] private ScalableButton rightButton;
    [SerializeField] private TMPro.TMP_Text mountAndYearText;

    private List<DateTime> months = new List<DateTime>();
    private int currentMonthIndex = -1;

    public event Action<DateTime> MonthSelected;

    private void Awake()
    {
        leftButton.OnClick.AddListener(OnLeftButtonPressed);
        rightButton.OnClick.AddListener(OnRightButtonPressed);
    }

    private void OnLeftButtonPressed()
    {
        SetMonth(currentMonthIndex - 1);
    }

    private void OnRightButtonPressed()
    {
        SetMonth(currentMonthIndex + 1);
    }

    public void SetMonths(List<DateTime> dates)
    {
        months = dates.ToList();
        if (months == null || months.Count == 0)
            months.Add(DateTime.Now);
        months.OrderBy(x => DateTime.Now - x).ToList();
        SetMonth(months.Count - 1);
    }

    public void SetMonth(int index)
    {
        currentMonthIndex = Mathf.Clamp(index, 0, months.Count - 1);
        var currentMonth = months[currentMonthIndex];
        mountAndYearText.text = currentMonth.ToString("MMMM yyyy");

        bool leftButtonEnabled = index > 0;
        bool rightButtonEnabled = index < (months.Count - 1);

        leftButton.SetState(leftButtonEnabled ? ScalableButton.State.Active : ScalableButton.State.Disabled);
        leftButton.interactable = leftButtonEnabled;
        rightButton.SetState(rightButtonEnabled ? ScalableButton.State.Active : ScalableButton.State.Disabled);
        rightButton.interactable = rightButtonEnabled;

        MonthSelected?.Invoke(currentMonth);
    }
}
