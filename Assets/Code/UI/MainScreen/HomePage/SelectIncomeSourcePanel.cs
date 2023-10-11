using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectIncomeSourcePanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton editButton;
    [SerializeField] private ScalableButton addButton;

    [SerializeField] private IncomeSourceView incomeSourceViewPrefab;
    [SerializeField] private RectTransform incomeSourcesParent;

    private Dictionary<IncomeSourceView, IncomeSource> views = new Dictionary<IncomeSourceView, IncomeSource>();

    public Action<CategoryType> OnChooseIncomeSource;

    private void Awake()
    {
        editButton.OnClick.AddListener(OnEdit);
        addButton.OnClick.AddListener(OnEdit);
        SaveData.Instance.IncomeSourceData.OnIncomeSourcesChanged += SetupIncomeSources;
    }

    private void OnEdit()
    {

    }

    private void OnAdd()
    {

    }

    private void SetupIncomeSources()
    {
        ClearIncomeSources();
        incomeSourceViewPrefab.gameObject.SetActive(false);
        string selectedSourceId = SaveData.Instance.IncomeSourceData.SelectedIncomeSourceId;

        foreach (var source in AppSettingsData.Instance.IncomeSourceSettings.IncomeSources)
        {
            IncomeSourceView view = Instantiate(incomeSourceViewPrefab, incomeSourcesParent);
            view.gameObject.SetActive(true);
            view.Initialize(source);
            view.SetIsSelected(source.Id == selectedSourceId);
            view.OnSelected += OnSelectAccount;
            views[view] = source;
        }
    }

    private void OnSelectAccount(IncomeSourceView selectedView)
    {
        SaveData.Instance.IncomeSourceData.SelectedIncomeSourceId = views[selectedView].Id;
        foreach (var view in views)
        {
            view.Key.SetIsSelected(SaveData.Instance.AccountsData.SelectedAccountId == view.Value.Id);
        }
        Hide(false);
    }


    private void ClearIncomeSources()
    {
        foreach (var view in views)
        {
            Destroy(view.Key.gameObject);
        }
        views.Clear();
    }


    public void Show(bool immediately = false)
    {
        bottomPanel.Show(immediately);

        SetupIncomeSources();
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }
}
