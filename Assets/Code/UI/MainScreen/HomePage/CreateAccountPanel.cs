using UnityEngine;

public class CreateAccountPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton closeButton;

    [Header("CreateAccountElements")]
    [SerializeField] private ScalableButton okButton;
    [SerializeField] private TMPro.TMP_InputField accountNameField;
    [SerializeField] private TMPro.TMP_InputField balanceField;
    [SerializeField] private ScalableButton chooseCurrencyButton;
    [SerializeField] private ScalableButton chooseIconButton;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        okButton.OnClick.AddListener(OnOkClicked);
    }

    private void OnOkClicked()
    {

    }

    public void Show(bool immediately = false)
    {
        bottomPanel.Show(immediately);
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }
}
