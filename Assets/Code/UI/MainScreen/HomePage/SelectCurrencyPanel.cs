using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelectCurrencyPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelRT;
    [SerializeField] private Graphic background;
    [SerializeField] private Color backgroundColor = Color.white;
    [SerializeField] private ScalableButton closeButton;
    [SerializeField] private CanvasGroup headerGroup;
    [SerializeField] private CanvasGroupFader headerDefaultStateGroupFader;
    [SerializeField] private CanvasGroupFader headerSearchStateGroupFader;

    [SerializeField] private CurrencyView currentCurrencyView;
    [SerializeField] private CurrencyView currencyViewPrefab;
    [SerializeField] private RectTransform infoParent;
    [SerializeField] private RectTransform currenciesParent;
    [SerializeField] private ScrollFadeHandler scrollFadeHandler;
    [SerializeField] private ScalableButton searchButton;
    [SerializeField] private ScalableButton searchBackButton;
    [SerializeField] private TMPro.TMP_InputField searchInputField;

    public event Action OnClose;
    public event Action<CurrencyType> OnCurrencySelected;

    private Dictionary<CurrencyView, CurrencyType> views = new Dictionary<CurrencyView, CurrencyType>();
    private CurrencyType selectedCurrency = CurrencyType.None;

    public CurrencyType SelectedCurrency => selectedCurrency;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        searchButton.OnClick.AddListener(() => SetSearchState());
        searchBackButton.OnClick.AddListener(() => SetDefaultState());
        searchInputField.onValueChanged.AddListener(OnSearchInputChanged);
    }

    private void OnChooseCurrency(CurrencyView choosedView)
    {
        if (views.ContainsKey(choosedView))
            SetSelectedCurrency(views[choosedView]);

        Hide(false);
    }

    public void SetSelectedCurrency(CurrencyType currencyType)
    {
        foreach (var view in views)
        {
            view.Key.SetIsSelected(view.Value == currencyType);
        }
        currentCurrencyView.Initialize(currencyType);
        OnCurrencySelected?.Invoke(currencyType);
        selectedCurrency = currencyType;    
    }

    private void SetupCurrencies()
    {
        ClearCurrencies();

        currentCurrencyView.SetIsSelected(true);
        currentCurrencyView.Initialize(AppSettingsData.Instance.CurrenciesSettings.DefaultSelectedCurrency);
        currencyViewPrefab.gameObject.SetActive(false);
        foreach (var currency in AppSettingsData.Instance.CurrenciesSettings.Currencies)
        {
            CurrencyView view = Instantiate(currencyViewPrefab, currenciesParent);
            view.gameObject.SetActive(true);
            view.SetIsSelected(currency.CurrencyType == AppSettingsData.Instance.CurrenciesSettings.DefaultSelectedCurrency);
            view.Initialize(currency.CurrencyType);
            view.OnSelected += OnChooseCurrency;
            views.Add(view, currency.CurrencyType);
        }
        ResetContentPosition();
    }

    private void ResetContentPosition()
    {
        currenciesParent.anchoredPosition = Vector2.zero;
        scrollFadeHandler.RecalculateFadersSize(Vector2.zero);
    }

    private void ClearCurrencies()
    {
        foreach (var view in views)
        {
            Destroy(view.Key.gameObject);
        }
        views.Clear();
    }

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this, true);
        headerGroup.gameObject.SetActive(true);
        panelRT.gameObject.SetActive(true);
        if (immediately)
        {
            headerGroup.alpha = 1;
            background.color = backgroundColor;
        }
        else
        {
            headerGroup.alpha = 0;
            headerGroup.DOFade(1f, 0.3f).SetDelay(0.15f).SetUpdate(true).SetId(this);
            background.DOColor(backgroundColor, 0.15f).SetUpdate(true).SetId(this);
            panelRT.anchoredPosition = new Vector2(panelRT.rect.width,
                panelRT.anchoredPosition.y);
            panelRT.DOAnchorPosX(0, 0.3f).SetUpdate(true).SetId(this);
        }

        SetDefaultState(true);
        SetupCurrencies();
    }

    public void Hide(bool immediately = false)
    {
        DOTween.Kill(this, true);
        if (immediately)
        {
            headerGroup.alpha = 0;
            headerGroup.gameObject.SetActive(false);
            panelRT.gameObject.SetActive(false);
        }
        else
        {
            headerGroup.DOFade(0f, 0.3f)
                .SetUpdate(true).SetId(this)
                .OnComplete(() => headerGroup.gameObject.SetActive(false));
            panelRT.DOAnchorPosX(panelRT.rect.width, 0.3f).SetUpdate(true).SetId(this)
                .OnComplete(() => panelRT.gameObject.SetActive(false));
        }
        OnClose?.Invoke();
    }

    private void OnSearchInputChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            value = string.Empty;

        value = value.Replace(" ", string.Empty);

        foreach (var view in views)
        {
            if (view.Key.Currency == null) continue;

            if(!string.IsNullOrEmpty(value) && value.Length == 1)
            {
                view.Key.gameObject.SetActive(view.Key.Currency.CurrencyName.Replace(" ", string.Empty).StartsWith(value[0].ToString(), StringComparison.CurrentCultureIgnoreCase)
               || view.Key.Currency.CurrencyFullName.Replace(" ", string.Empty).StartsWith(value[0].ToString(), StringComparison.CurrentCultureIgnoreCase)
               || view.Key.NameText.Replace(" ", string.Empty).StartsWith(value[0].ToString(), StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                view.Key.gameObject.SetActive(string.IsNullOrEmpty(value) 
               || view.Key.Currency.CurrencyName.Replace(" ", string.Empty).Contains(value, StringComparison.CurrentCultureIgnoreCase)
               || view.Key.Currency.CurrencyFullName.Replace(" ", string.Empty).Contains(value, StringComparison.CurrentCultureIgnoreCase)
               || view.Key.NameText.Replace(" ", string.Empty).Contains(value, StringComparison.CurrentCultureIgnoreCase));
            } 
        }
    }

    private void SetDefaultState(bool immediately = false)
    {
        if (immediately)
        {
            headerDefaultStateGroupFader.Show(immediately);
            headerSearchStateGroupFader.Hide(immediately);
        }
        else
        {
            headerDefaultStateGroupFader.Show(immediately, 0.15f);
            headerSearchStateGroupFader.Hide(immediately);
        }
        infoParent.gameObject.SetActive(true);
        OnSearchInputChanged(string.Empty);
        ResetContentPosition();
    }

    private void SetSearchState(bool immediately = false)
    {
        if (immediately)
        {
            headerDefaultStateGroupFader.Hide(immediately);
            headerSearchStateGroupFader.Show(immediately);
        }
        else
        {
            headerDefaultStateGroupFader.Hide(immediately);
            headerSearchStateGroupFader.Show(immediately, 0.15f);
        }
        infoParent.gameObject.SetActive(false);
        searchInputField.text = string.Empty;
    }
}

