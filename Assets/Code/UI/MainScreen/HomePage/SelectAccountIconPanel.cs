using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAccountIconPanel : MonoBehaviour
{
    [SerializeField] private ScalableButton closeButton;

    [SerializeField] private RectTransform currenciesParent;
    [SerializeField] private ScalableButton searchButton;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        searchButton.OnClick.AddListener(OnSearchClicked);
    }

    private void OnSearchClicked()
    {

    }

    private void OnChooseCurrency()
    {

    }

    public void Show(bool immediately = false)
    {
        
    }

    public void Hide(bool immediately = false)
    {
        
    }
}