using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabBar : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private float time;
    [SerializeField] private RectTransform pagesRT;
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
            DeselectPage(tabPage, true);
        }
    }

    private void OnEnable()
    {
        if (tabPages.Count > 0) OpenPage(tabPages[0], true);
    }

    private void OnTabButtonPressed(TabPage chosedPage)
    {
        OpenPage(chosedPage);
    }

    public void OpenPage(TabPage page, bool immidiately = false)
    {
        if (currentPage == page) return;

        int moveSide = tabPages.IndexOf(currentPage) > tabPages.IndexOf(page) ? 1 : -1;

        DeselectPage(currentPage, immidiately, moveSide);
        currentPage = page;
        SelectPage(page, immidiately, -moveSide);
    }

    private void DeselectPage(TabPage page, bool immidiately = false, int moveSide = 1)
    {
        if (page == null) return;

        DOTween.Kill(page.pageRT, true);

        page.tabButton.SetState(ScalableButton.State.Active);

        if (immidiately)
        {
            page.pageRT.gameObject.SetActive(false);
        }
        else
        {
            page.pageRT.DOAnchorPosX(pagesRT.rect.width * moveSide, time)
                .SetEase(ease)
                .SetId(page.pageRT)
                .SetUpdate(true)
                .OnComplete(() => page.pageRT.gameObject.SetActive(false));
        }
    }

    private void SelectPage(TabPage page, bool immidiately = false, int moveSide = 1)
    {
        if (page == null) return;

        DOTween.Kill(page.pageRT, true);

        page.pageRT.gameObject.SetActive(true);
        page.tabButton.SetState(ScalableButton.State.Selected);

        if (!immidiately)
        {
            page.pageRT.anchoredPosition = new Vector2(pagesRT.rect.width * moveSide, page.pageRT.anchoredPosition.y);
            page.pageRT.DOAnchorPosX(0, time)
                .SetEase(ease)
                .SetId(page.pageRT)
                .SetUpdate(true);
        }
    }
}
