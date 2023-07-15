using DG.Tweening;
using UnityEngine;

public class CreateAccountPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton closeButton;
    [SerializeField] private CanvasGroup headerGroup;
    [SerializeField] private RectTransform contentRT;

    [SerializeField] private ScalableButton okButton;
    [SerializeField] private TMPro.TMP_InputField accountNameField;
    [SerializeField] private TMPro.TMP_InputField balanceField;
    [SerializeField] private ScalableButton chooseCurrencyButton;
    [SerializeField] private ScalableButton chooseIconButton;

    [SerializeField] private SelectCurrencyPanel selectCurrencyPanel;

    private AccountIconType selectedIcon = AccountIconType.None;
    private CurrencyType selectedCurrency = CurrencyType.None;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        okButton.OnClick.AddListener(OnOkClicked);
        chooseCurrencyButton.OnClick.AddListener(OnChooseCurrency);
        chooseIconButton.OnClick.AddListener(OnChooseIcon);
    }

    private void OnEnable()
    {
        selectCurrencyPanel.OnClose += OnSubPanelClose;
    }

    private void OnDisable()
    {
        selectCurrencyPanel.OnClose -= OnSubPanelClose;
    }

    private void OnOkClicked()
    {
        if (!ValidateAccountData()) return;

        bool balanceValid = float.TryParse(balanceField.text, out float balance);
        Account account = new Account(accountNameField.text, accountNameField.text, selectedIcon,
            selectedCurrency, balanceValid ? balance : 0f);

        SaveData.Instance.AccountsData.AddAccount(account);
    }

    private bool ValidateAccountData()
    {
        return true;
    }

    private void OnChooseCurrency()
    {
        selectCurrencyPanel.Show(false);
        HideContent();
        
    }

    private void OnChooseIcon()
    {

    }

    private void OpenPanel(RectTransform panel)
    {

    }

    private void ClosePanel(RectTransform panel)
    {

    }

    public void Show(bool immediately = false)
    {
        selectCurrencyPanel.Hide(true);

        bottomPanel.Show(immediately);
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }

    private void OnSubPanelClose()
    {
        ShowContent();
    }

    private void ShowContent(bool immediately = false)
    {
        DOTween.Kill(this, true);
        contentRT.gameObject.SetActive(true);
        headerGroup.gameObject.SetActive(true);
        if (immediately)
        {
            contentRT.anchoredPosition = new Vector2(0f, contentRT.anchoredPosition.y);
            headerGroup.alpha = 1f;
        }
        else
        {
            headerGroup.DOFade(1f, 0.15f).SetDelay(0.15f)
               .SetUpdate(true).SetId(this);
            contentRT.DOAnchorPosX(0f, 0.3f).SetUpdate(true).SetId(this);
        }
    }

    private void HideContent(bool immediately = false)
    {
        DOTween.Kill(this, true);
        if (immediately)
        {
            headerGroup.gameObject.SetActive(false);
            headerGroup.alpha = 0f;
            contentRT.gameObject.SetActive(false);
        }
        else
        {
            headerGroup.DOFade(0f, 0.3f)
               .SetUpdate(true).SetId(this)
               .OnComplete(() => headerGroup.gameObject.SetActive(false));
            contentRT.DOAnchorPosX(-contentRT.rect.width, 0.3f).SetUpdate(true).SetId(this)
                .OnComplete(() => contentRT.gameObject.SetActive(false));
        }
    }
}
