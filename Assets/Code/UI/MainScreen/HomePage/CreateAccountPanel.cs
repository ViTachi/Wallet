using UnityEngine;

public class CreateAccountPanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton closeButton;

    private void Awake()
    {
        closeButton.OnClick.AddListener(() => Hide(false));
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
