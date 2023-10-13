using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectIncomeSourcePanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private RectTransform selectStateHeader;
    [SerializeField] private RectTransform editStateHeader;
    [SerializeField] private ScalableButton editButton;
    [SerializeField] private ScalableButton addButton;
    [SerializeField] private ScalableButton editCompleteButton;

    [SerializeField] private IncomeSourceView incomeSourceViewPrefab;
    [SerializeField] private RectTransform incomeSourcesParent;

    private Dictionary<IncomeSourceView, IncomeSource> views = new Dictionary<IncomeSourceView, IncomeSource>();

    public Action<CategoryType> OnChooseIncomeSource;

    private State currentState = State.None;

    private enum State { None, Select, Edit }

    private void Awake()
    {
        editButton.OnClick.AddListener(OnEdit);
        addButton.OnClick.AddListener(OnAdd);
        editCompleteButton.OnClick.AddListener(OnEditComplete);
        SaveData.Instance.IncomeSourceData.OnIncomeSourcesChanged += SetupIncomeSources;
    }

    private void OnEdit()
    {
        SetState(State.Edit);       
    }

    private void OnEditComplete()
    {
        SetState(State.Select);
    }

    private void OnAdd()
    {

    }

    private void SetupIncomeSources()
    {
        ClearIncomeSources();
        incomeSourceViewPrefab.gameObject.SetActive(false);
        string selectedSourceId = SaveData.Instance.IncomeSourceData.SelectedIncomeSourceId;

        foreach (var source in SaveData.Instance.IncomeSourceData.IncomeSources)
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
        switch (currentState)
        {
            case State.None:
                break;
            case State.Select:
                SaveData.Instance.IncomeSourceData.SelectedIncomeSourceId = views[selectedView].Id;
                foreach (var view in views)
                {
                    view.Key.SetIsSelected(SaveData.Instance.AccountsData.SelectedAccountId == view.Value.Id);
                }
                Hide(false);
                break;
            case State.Edit:
                if (SaveData.Instance.IncomeSourceData.IncomeSources.Count < 2) break;

                ConfirmationPopup confirmationPopup = UIScreens.Instance.GetElement<ConfirmationPopup>();
                confirmationPopup.Initialize((delete) => {
                    if (delete)
                    {
                        SaveData.Instance.IncomeSourceData.RemoveIncomeSource(views[selectedView].Id);
                    }
                }, "Delete this Income?");
                confirmationPopup.Show();
                break;
            default:
                break;
        }
    }

    private void SetState(State state)
    {
        currentState = state;

        selectStateHeader.gameObject.SetActive(false);
        editStateHeader.gameObject.SetActive(false);

        addButton.gameObject.SetActive(false);
        editCompleteButton.gameObject.SetActive(false);

        switch (state)
        {
            case State.None:
                break;
            case State.Select:
                addButton.gameObject.SetActive(true);
                selectStateHeader.gameObject.SetActive(true);
                foreach (var view in views.Keys)
                    view.SetRemoveButtonActive(false);
                break;
            case State.Edit:
                editCompleteButton.gameObject.SetActive(true);
                editStateHeader.gameObject.SetActive(true);
                foreach (var view in views.Keys)
                    view.SetRemoveButtonActive(true);
                break;
            default:
                break;
        }
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
        SetState(State.Select);
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }
}
