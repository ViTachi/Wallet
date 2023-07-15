using UnityEngine;

[CreateAssetMenu(menuName = "Data/Icon/AccountIcon", fileName = "AccountIcon")]
public class AccountIcon : IconData
{
    [SerializeField] private AccountIconType accountIconType;
    public AccountIconType AccountIconType => accountIconType;
}

public enum AccountIconType
{
    None, Money, Visa, Bitcoin
}
