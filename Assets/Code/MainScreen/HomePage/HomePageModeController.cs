using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HomePageModeController : MonoBehaviour
{
    [SerializeField] private ModeSlider modeSlider;
    [SerializeField] private Button transferButton;
    [SerializeField] private Button addButton;

    public enum Mode { Expense = 1, Income = 2, Transfer  = 3 }

    private Mode currentMode;

    private void Start()
    {
      
        //Invoke(nameof(OnAtorIK), 1);
        
        StartCoroutine(S());
    }

    private IEnumerator S()
    {
        yield return null;
        modeSlider.SetMode(1, true);
    }

    private void OnEnable()
    {
        modeSlider.OnModeChanged += OnModeChanged;
    }

    private void OnDisable()
    {
        modeSlider.OnModeChanged -= OnModeChanged;
    }

    private void SetMode(Mode mode)
    {
        SetDefaults();
        switch (mode)
        {
            case Mode.Expense:
                addButton.gameObject.SetActive(true);
                break;
            case Mode.Income:
                addButton.gameObject.SetActive(true);
                break;
            case Mode.Transfer:
                transferButton.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void OnModeChanged(int modeNumber)
    {
        Mode mode = (Mode)modeNumber;
        if (currentMode != mode) SetMode(mode);      
    }

    private void SetDefaults()
    {
        transferButton.gameObject.SetActive(false);
        addButton.gameObject.SetActive(false);
    }
}
