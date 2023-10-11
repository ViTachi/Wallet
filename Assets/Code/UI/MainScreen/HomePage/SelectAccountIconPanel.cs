using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SelectAccountIconPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelRT;
    [SerializeField] private ScalableButton closeButton;
    [SerializeField] private ScalableButton okButton;
    [SerializeField] private CanvasGroup headerGroup;
    [SerializeField] private IconView headerIconView;

    [SerializeField] private AccountIconView iconViewPrefab;
    [SerializeField] private RectTransform iconsParent;
    [SerializeField] private ScrollFadeHandler scrollFadeHandler;

    [SerializeField] private int colorChooseViewCount = 5;
    [SerializeField] private ScalableButton chooseColorButton;
    [SerializeField] private ScalableButton chooseColorBackButton;
    [SerializeField] private ColorPickView colorPickViewPrefab;
    [SerializeField] private ColorPickView colorPickViewPrefab2;
    [SerializeField] private RectTransform colorPickViewsParent;
    [SerializeField] private RectTransform colorPickViewsParent2;

    [SerializeField] private RectTransform chooseIconContent;
    [SerializeField] private RectTransform chooseColorContent;
    [SerializeField] private CanvasGroupFader chooseIconHeaderFader;
    [SerializeField] private CanvasGroupFader chooseColorHeaderFader;

    public event Action OnClose;
    public event Action<AccountIconType> OnIconSelected;
    public event Action<IconColorType> OnColorSelected;

    private Dictionary<AccountIconView, AccountIconType> iconViews = new Dictionary<AccountIconView, AccountIconType>();
    private AccountIconType selectedIcon = AccountIconType.None;

    private Dictionary<ColorPickView, IconColorType> colorViews = new Dictionary<ColorPickView, IconColorType>();
    private IconColorType selectedColorType = IconColorType.None;

    public AccountIconType SelectedIcon => selectedIcon;
    public IconColorType SelectedColorType => selectedColorType;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        okButton.OnClick.AddListener(OnOkClocked);
        chooseColorButton.OnClick.AddListener(() => SetChooseColorState());
        chooseColorBackButton.OnClick.AddListener(() => SetChooseIconState());
    }

    private void OnChooseIcon(AccountIconView choosedView)
    {
        if(iconViews.ContainsKey(choosedView))
            SetSelectedIcon(iconViews[choosedView]);
    }

    public void SetSelectedIcon(AccountIconType accountIconType)
    {
        foreach (var view in iconViews)
        {
            view.Key.SetIsSelected(view.Value == accountIconType);
        }

        selectedIcon = accountIconType;

        AccountIcon accountIcon = AppSettingsData.Instance.AccountsSettings.GetAccountIcon(accountIconType);
        if (accountIcon != null)
            headerIconView.SetIcon(accountIcon.Icon);    
    }

    private void SetupIcons()
    {
        ClearIcons();

        iconViewPrefab.gameObject.SetActive(false);
        foreach (var icon in AppSettingsData.Instance.AccountsSettings.AccountIcons)
        {
            AccountIconView view = Instantiate(iconViewPrefab, iconsParent);
            view.gameObject.SetActive(true);
            view.Initialize(icon.AccountIconType);
            view.OnSelected += OnChooseIcon;
            iconViews.Add(view, icon.AccountIconType);
        }

        ResetContentPosition();
    }

    private void ResetContentPosition()
    {
        iconsParent.anchoredPosition = Vector2.zero;
        scrollFadeHandler.RecalculateFadersSize(Vector2.zero);
    }

    private void ClearIcons()
    {
        foreach (var view in iconViews)
        {
            Destroy(view.Key.gameObject);
        }
        iconViews.Clear();
    }

    private void OnChooseColor(ColorPickView choosedView)
    {
        if (colorViews.ContainsKey(choosedView))
            SetSelectedColor(colorViews[choosedView]);
    }

    public void SetSelectedColor(IconColorType colorType)
    {
        foreach (var view in colorViews)
        {
            view.Key.SetIsSelected(view.Value == colorType);
        }

        selectedColorType = colorType;

        IconColor iconColor = AppSettingsData.Instance.GetIconColor(colorType);
        if (iconColor != null)
        {
            foreach (var view in iconViews)
            {
                view.Key.SetColor(iconColor.Color);
            }
            headerIconView.SetColor(iconColor.Color);
        }
    }

    private void SetupColors()
    {
        ClearColors();

        colorPickViewPrefab.gameObject.SetActive(false);
        var colors = AppSettingsData.Instance.IconColors;
        for (int i = 0; i < colors.Count; i++)
        {
            if (i >= colorChooseViewCount) break;

            ColorPickView view = Instantiate(colorPickViewPrefab, colorPickViewsParent);
            view.gameObject.SetActive(true);
            view.Initialize(colors[i].Color);
            view.OnSelected += OnChooseColor;
            colorViews.Add(view, colors[i].IconColorType);
        }
        colorPickViewPrefab2.gameObject.SetActive(false);
        for (int i = 0; i < colors.Count; i++)
        {
            ColorPickView view = Instantiate(colorPickViewPrefab2, colorPickViewsParent2);
            view.gameObject.SetActive(true);
            view.Initialize(colors[i].Color);
            view.OnSelected += OnChooseColor;
            colorViews.Add(view, colors[i].IconColorType);
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

    private void OnOkClocked()
    {
        if(selectedIcon != AccountIconType.None) 
            OnIconSelected?.Invoke(selectedIcon);
        if (selectedColorType != IconColorType.None)
            OnColorSelected?.Invoke(selectedColorType);
        Hide(false);
    }

    private void SetChooseIconState(bool immediately = false)
    {
        chooseIconContent.gameObject.SetActive(true);
        chooseColorContent.gameObject.SetActive(false);
        chooseIconHeaderFader.Show(immediately, 0.15f);
        chooseColorHeaderFader.Hide(immediately);
    }

    private void SetChooseColorState(bool immediately = false)
    {
        chooseIconContent.gameObject.SetActive(false);
        chooseColorContent.gameObject.SetActive(true);
        chooseIconHeaderFader.Hide(immediately);
        chooseColorHeaderFader.Show(immediately, 0.15f);
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
        SetChooseIconState(true);
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