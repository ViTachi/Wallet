using System.Collections.Generic;
using UnityEngine;

public class TabBar : MonoBehaviour
{
    [SerializeField] private List<TabPage> tabPages;

    private TabPage currentPage = null;

    [System.Serializable]
    public class TabPage
    {
        public RectTransform pageRT;
        public ScalableButton tabButton;
    }

    private void Awake()
    {
        foreach (var tabPage in tabPages)
        {
            tabPage.tabButton.OnClick.AddListener(() => OnTabButtonPressed(tabPage));
            DeselectPage(tabPage);
        }
    }

    private void OnEnable()
    {
        if (tabPages.Count > 0) OpenPage(tabPages[0]);
    }

    private void OnTabButtonPressed(TabPage chosedPage)
    {
        OpenPage(chosedPage);
    }

    public void OpenPage(TabPage page)
    {
        if (currentPage == page) return;

        DeselectPage(currentPage);
        currentPage = page;
        SelectPage(page);
    }

    private void DeselectPage(TabPage page)
    {
        if (page == null) return;
      
        page.tabButton.SetState(ScalableButton.State.Active);
        page.pageRT.gameObject.SetActive(false);
    }

    private void SelectPage(TabPage page)
    {
        if (page == null) return;

        page.pageRT.gameObject.SetActive(true);
        page.tabButton.SetState(ScalableButton.State.Selected);
    }
}
