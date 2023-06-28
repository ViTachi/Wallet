using System.Collections;
using UnityEngine;

public class HomePageModeController : MonoBehaviour
{
    [SerializeField] private ValueInputController valueInputController;
    [SerializeField] private ModeSlider modeSlider;
    [SerializeField] private ScalableButton transferButton;
    [SerializeField] private ScalableButton selectAccountButton;

    [SerializeField] private ScalableButton addButton;
    [SerializeField] private ScalableButton noteButton;
    [SerializeField] private ScalableButton calendarButton;


    public enum Mode { None = 0, Expense = 1, Income = 2, Transfer = 3 }

    private Mode currentMode;

    private OperationsManager operationsManager;
    private NotePanel notePanel;
    private CategorySelectPanel categorySelectPanel;

    private void Awake()
    {
        addButton.OnClick.AddListener(OnAdd);
        transferButton.OnClick.AddListener(OnTransfer);
        selectAccountButton.OnClick.AddListener(OpenSelectAccountPanel);
        noteButton.OnClick.AddListener(OpenNote);
        calendarButton.OnClick.AddListener(OpenCalendar);

        notePanel = UIScreens.Instance.GetElement<NotePanel>();
        categorySelectPanel = UIScreens.Instance.GetElement<CategorySelectPanel>();
        operationsManager = OperationsManager.Instance;
    }

    private void Start()
    {
        addButton.SetState(ScalableButton.State.Disabled);
        valueInputController.ResetContoller();
        StartCoroutine(SetupLayout());
    }

    private IEnumerator SetupLayout()
    {
        yield return null;
        modeSlider.SetMode(1, true);
        OnPremiumModeChanged(PremiumMode.CurrentMode);
    }

    private void OnEnable()
    {
        modeSlider.OnModeChanged += OnModeChanged;
        valueInputController.OnChangeValue += OnValueChanged;
        PremiumMode.OnModeChanged += OnPremiumModeChanged;
    }

    private void OnDisable()
    {
        modeSlider.OnModeChanged -= OnModeChanged;
        valueInputController.OnChangeValue -= OnValueChanged;
        PremiumMode.OnModeChanged -= OnPremiumModeChanged;
    }

    private void OpenSelectAccountPanel()
    {
        UIScreens.Instance.ShowElement<SelectAccountPanel>();
    }

    private void OpenNote()
    {
        notePanel.SetDeleteButtonActive(!operationsManager.IsOperationNoteEmpty);
        notePanel.SetText(operationsManager.Note);
        notePanel.Show();
        notePanel.OnDone += AddNoteToOperation;
        notePanel.OnDelete += DeleteNote;
        notePanel.OnClose += OnNoteClose;
    }

    private void OnNoteClose()
    {
        notePanel.OnDone -= AddNoteToOperation;
        notePanel.OnDelete -= DeleteNote;
        notePanel.OnClose -= OnNoteClose;
    }

    private void AddNoteToOperation()
    {
        operationsManager.SetNote(notePanel.NoteText);
    }

    private void DeleteNote()
    {
        operationsManager.DeleteNote();
    }

    private void OnAdd()
    {
        if (valueInputController.CurrentVulue == 0) return;
        operationsManager.SetValue(valueInputController.CurrentVulue);
        OpenCategorySelect();
    }

    private void OnTransfer()
    {
        if (valueInputController.CurrentVulue == 0) return;
    }

    private void OpenCategorySelect()
    {
        categorySelectPanel.Show();
    }

    private void OpenCalendar()
    {

    }

    private void SaveOperation()
    {
        operationsManager.SaveOperation();
    }

    private void OnValueChanged(float value)
    {
        if (value == 0)
        {
            transferButton.SetState(ScalableButton.State.Disabled);
            addButton.SetState(ScalableButton.State.Disabled);
        }
        else
        {
            transferButton.SetState(ScalableButton.State.Active);
            addButton.SetState(ScalableButton.State.Active);
        }
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

    private void OnPremiumModeChanged(PremiumMode.Mode mode)
    {
        modeSlider.SetModeVisibility(3, mode == PremiumMode.Mode.Premium);
    }
}
