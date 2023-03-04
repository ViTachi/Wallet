using UnityEngine;

public class CategorySelectPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton editButton;
    [SerializeField] private ScalableButton closeButton;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
        editButton.OnClick.AddListener(OnEdit);
    }

    private void OnEdit()
    {

    }

    public void Show(bool immediately = false)
    {
        bottomPanel.Show(immediately);
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
    }
}
