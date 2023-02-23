using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabBar : MonoBehaviour
{
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private List<TabPage> tabPages;

    private TabPage currentPage = null;

    [System.Serializable]
    public class TabPage
    {
        public RectTransform pageRT;
        public Button tabButton;
    }

    private void Awake()
    {
        foreach (var tabPage in tabPages)
        {
            tabPage.tabButton.onClick.AddListener(() => OnTabButtonPressed(tabPage));
            DeselectPage(tabPage);
        }
    }

    private void OnEnable()
    {
        if(tabPages.Count > 0) OpenPage(tabPages[0]);
    }

    private void OnTabButtonPressed(TabPage chosedPage)
    {
        OpenPage(chosedPage);
    }

    public void OpenPage(TabPage page)
    {
        DeselectPage(currentPage);
        currentPage = page;
        SelectPage(page);
    }

    private void DeselectPage(TabPage page)
    {
        if (page == null) return;

        DOTween.Kill(this, true);

        page.tabButton.targetGraphic.color = normalColor;
        page.pageRT.gameObject.SetActive(false);        
    }

    private void SelectPage(TabPage page)
    {
        if (page == null) return;

        DOTween.Kill(this, true);

        page.tabButton.targetGraphic.color = selectedColor;
        page.pageRT.gameObject.SetActive(true);
    }
}
