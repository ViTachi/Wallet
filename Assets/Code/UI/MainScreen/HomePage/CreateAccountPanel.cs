using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton closeButton;
    [SerializeField] private Graphic background;
    [SerializeField] private Color backgroundColor = Color.white;
    [SerializeField] private CanvasGroup headerGroup;
    [SerializeField] private RectTransform contentRT;
    [SerializeField] private TMPro.TMP_Text selectedCurrencyText;

    [SerializeField] private ScalableButton okButton;
    [SerializeField] private TMPro.TMP_InputField accountNameField;
    [SerializeField] private TMPro.TMP_InputField balanceField;
    [SerializeField] private IconView selectedIconView;
    [SerializeField] private ScalableButton chooseCurrencyButton;
    [SerializeField] private ScalableButton chooseIconButton;

    [SerializeField] private SelectCurrencyPanel selectCurrencyPanel;
    [SerializeField] private SelectAccountIconPanel selectAccountIconPanel;

    private AccountIconType selectedIcon = AccountIconType.None;
    private CurrencyType selectedCurrency = CurrencyType.None;
    private int selectedColorIndex = 0;

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
        selectAccountIconPanel.OnClose += OnSubPanelClose;
        selectCurrencyPanel.OnCurrencySelected += SetSelectedCurrency;
        selectAccountIconPanel.OnIconSelected += SetSelectedIcon;
        selectAccountIconPanel.OnColorSelected += SetSelectedColor;
    }

    private void OnDisable()
    {
        selectCurrencyPanel.OnClose -= OnSubPanelClose;
        selectAccountIconPanel.OnClose -= OnSubPanelClose;
        selectCurrencyPanel.OnCurrencySelected -= SetSelectedCurrency;
        selectAccountIconPanel.OnIconSelected -= SetSelectedIcon;
        selectAccountIconPanel.OnColorSelected -= SetSelectedColor;
    }

    private void SetSelectedCurrency(CurrencyType currencyType)
    {
        selectedCurrency = currencyType;
        Currency currency = AppSettingsData.Instance.CurrenciesSettings.GetCurrency(currencyType);
        if(currency != null)
        {
            selectedCurrencyText.text = currency.IconText != null ? currency.IconText : currency.CurrencyName;
        }        
    }

    private void SetSelectedIcon(AccountIconType iconType)
    {
        selectedIcon = iconType;
        AccountIcon icon = AppSettingsData.Instance.AccountsSettings.GetAccountIcon(iconType);
        if(icon != null)
            selectedIconView.SetIcon(icon.Icon);
    }

    private void SetSelectedColor(int colorIndex)
    {
        selectedColorIndex = colorIndex;
        Color color = AppSettingsData.Instance.AccountsSettings.GetIconColor(colorIndex);
        if (color != default)
            selectedIconView.SetColor(color);
    }

    private void OnOkClicked()
    {
        if (!ValidateAccountData()) return;

        bool balanceValid = float.TryParse(balanceField.text, out float balance);
        Account account = new Account(accountNameField.text, accountNameField.text, selectedIcon,
            selectedColorIndex, selectedCurrency, balanceValid ? balance : 0f);

        SaveData.Instance.AccountsData.AddAccount(account);

        Hide(false);
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
        selectAccountIconPanel.Show(false);
        HideContent();
    }

    public void Show(bool immediately = false)
    {
        bottomPanel.Show(immediately);
        selectCurrencyPanel.Hide(true);
        selectAccountIconPanel.Hide(true);

        ShowContent(true);
        SetSelectedCurrency(AppSettingsData.Instance.CurrenciesSettings.DefaultSelectedCurrency);
        if(AppSettingsData.Instance.AccountsSettings.AccountIcons.Count > 0)
            SetSelectedIcon(AppSettingsData.Instance.AccountsSettings.AccountIcons[0].AccountIconType);
        SetSelectedColor(0);
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
            background.color = backgroundColor;
        }
        else
        {
            headerGroup.DOFade(1f, 0.3f).SetDelay(0.15f)
               .SetUpdate(true).SetId(this);
            background.DOColor(backgroundColor, 0.15f).SetUpdate(true).SetId(this);
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
