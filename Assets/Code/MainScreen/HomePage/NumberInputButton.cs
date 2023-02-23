using UnityEngine;
using UnityEngine.UI;

public class NumberInputButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int number;

    public Button Button => button;
    public int Number => number;
}
