using UnityEngine;

[CreateAssetMenu(menuName = "Data/Currency", fileName = "Currency")]
public class Currency : ScriptableObject
{
    [SerializeField] private CurrencyType currencyType;
    [SerializeField] private string currencyName;
    [SerializeField] private Sprite icon;
    [SerializeField] private string iconText;

    public CurrencyType CurrencyType => currencyType;
    public string CurrencyName => currencyName;
    public Sprite Icon => icon;
    public string IconText => iconText;
}

public enum CurrencyType
{
    USD,
    RUB,
    BYR,
    EUR,
}
