using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelectAccountIconPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelRT;
    [SerializeField] private ScalableButton closeButton;
    [SerializeField] private CanvasGroup headerGroup;
    [SerializeField] private IconView headerIconView;

    [SerializeField] private AccountIconView iconViewPrefab;
    [SerializeField] private RectTransform iconsParent;
    [SerializeField] private ScrollFadeHandler scrollFadeHandler;
    [SerializeField] private List<RectTransform> emptyViews;

    [SerializeField] private int colorChooseViewCount = 5;
    [SerializeField] private ScalableButton chooseColorButton;
    [SerializeField] private ColorPickView colorPickViewPrefab;
    [SerializeField] private RectTransform colorPickViewsParent;

    public event Action OnClose;
    public event Action<AccountIconType> OnIconSelected;
    public event Action<int> OnColorSelected;

    private Dictionary<AccountIconView, AccountIconType> iconViews = new Dictionary<AccountIconView, AccountIconType>();
    private AccountIconType selectedIcon = AccountIconType.None;

    private Dictionary<ColorPickView, int> colorViews = new Dictionary<ColorPickView, int>();
    private int selectedColorIndex = 0;

    public AccountIconType SelectedIcon => selectedIcon;
    public int SelectedColorIndex => selectedColorIndex;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
    }

    private void OnChooseIcon(AccountIconView choosedView)
    {
        foreach (var view in iconViews)
        {
            view.Key.SetIsSelected(view.Key == choosedView);
            if (view.Key == choosedView)
            {
                OnIconSelected?.Invoke(view.Value);
                selectedIcon = view.Value;
            }
        }
        AccountIcon accountIcon = AppSettingsData.Instance.AccountsSettings.GetAccountIcon(iconViews[choosedView]);
        if (accountIcon != null)
            headerIconView.SetIcon(accountIcon.Icon);
    }

    private void SetupIcons()
    {
        ClearCurrencies();

        iconViewPrefab.gameObject.SetActive(false);
        foreach (var icon in AppSettingsData.Instance.AccountsSettings.AccountIcons)
        {
            AccountIconView view = Instantiate(iconViewPrefab, iconsParent);
            view.gameObject.SetActive(true);
            view.SetIsSelected(icon.AccountIconType == AppSettingsData.Instance.AccountsSettings.AccountIcons[0].AccountIconType);
            view.Initialize(icon.AccountIconType);
            view.OnSelected += OnChooseIcon;
            iconViews.Add(view, icon.AccountIconType);
        }

        for(int i = 0; i < emptyViews.Count; i++)
        {
            emptyViews[i].SetSiblingIndex(iconViews.Count + emptyViews.Count);
            emptyViews[i].gameObject.SetActive(iconViews.Count + i <= 4);
        }

        ResetContentPosition();
    }

    private void ResetContentPosition()
    {
        iconsParent.anchoredPosition = Vector2.zero;
        scrollFadeHandler.RecalculateFadersSize(Vector2.zero);
    }

    private void ClearCurrencies()
    {
        foreach (var view in iconViews)
        {
            Destroy(view.Key.gameObject);
        }
        iconViews.Clear();
    }

    private void OnChooseColor(ColorPickView choosedView)
    {
        foreach (var view in colorViews)
        {
            view.Key.SetIsSelected(view.Key == choosedView);
            if (view.Key == choosedView)
            {
                OnColorSelected?.Invoke(view.Value);
                selectedColorIndex = view.Value;
            }
        }
        Color color = AppSettingsData.Instance.AccountsSettings.GetIconColor(selectedColorIndex);
        if (color != default)
        {
            foreach (var view in iconViews)
            {
                view.Key.SetColor(color);
            }
            headerIconView.SetColor(color);
        }
    }

    private void SetupColors()
    {
        ClearColors();

        colorPickViewPrefab.gameObject.SetActive(false);
        var colors = AppSettingsData.Instance.AccountsSettings.IconColors;
        for (int i = 0; i < colors.Count; i++)
        {
            if (i >= colorChooseViewCount) break;

            ColorPickView view = Instantiate(colorPickViewPrefab, colorPickViewsParent);
            view.gameObject.SetActive(true);
            view.SetIsSelected(i == 0);
            view.Initialize(colors[i]);
            view.OnSelected += OnChooseColor;
            colorViews.Add(view, i);
        }
    }

    private void ClearColors()
    {
        foreach (var view in colorViews)
        {
            Destroy(view.Key.gameObject);
        }
        colorViews.Clear();
    }

    public void Show(bool immediately = false)
    {
        DOTween.Kill(this, true);
        headerGroup.gameObject.SetActive(true);
        panelRT.gameObject.SetActive(true);
        if (immediately)
        {
            headerGroup.alpha = 1;
        }
        else
        {
            headerGroup.alpha = 0;
            headerGroup.DOFade(1f, 0.3f).SetDelay(0.15f).SetUpdate(true).SetId(this);
            panelRT.anchoredPosition = new Vector2(panelRT.rect.width,
                panelRT.anchoredPosition.y);
            panelRT.DOAnchorPosX(0, 0.3f).SetUpdate(true).SetId(this);
        }

        SetupIcons();
        SetupColors();
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
}