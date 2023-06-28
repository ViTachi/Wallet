using UnityEngine;

[CreateAssetMenu(menuName = "Data/Icon/IncomeSourceIcon", fileName = "IncomeSourceIcon")]
public class IncomeSourceIcon : IconData
{
    [SerializeField] private IncomeSourceIconType incomeSourceIconType;
    public IncomeSourceIconType IncomeSourceIconType => incomeSourceIconType;
}

public enum IncomeSourceIconType
{
    Salary, Savings, Deposits
}
