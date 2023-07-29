
using UnityEngine;

public class ModelTest : MonoBehaviour
{

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    //Test();
        //    var screen = UIScreens.Instance.GetElement<DeletePopup>();
        //    screen.Hide();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    var screen = UIScreens.Instance.GetElement<DeletePopup>();
        //    screen.Initialize((b) => Debug.Log($"{b} rrr"), "Ttttt", DeletePopup.Variant.Delete);
        //    screen.Show();
        //    //Read();
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SaveData.Instance.AccountsData.AddAccount(new Account("ac4", "Account n4", AccountIconType.Visa, CurrencyType.BYR, 120.5f));
        //    //Read();
        //}
    }

    public void Read()
    {
        foreach (var item in SaveData.Instance.AccountsData.Accounts)
        {
            Debug.Log($"{item.Icon}");
            Debug.Log($"{item.Id}");
        }
    }

    //private void Test()
    //{
    //    SaveData.Instance.IncomeSourceData.AddIncomeSource(new IncomeSource("id_0", "source1", IncomeSourceIconType.Salary));
    //    SaveData.Instance.AccountsData.AddAccount(new Account("id_0", "Cash", AccountIconType.Bitcoin, CurrencyType.BYR, 100f));
    //    SaveData.Save();
    //    Debug.Log("Save Complete!");
    //}
}
