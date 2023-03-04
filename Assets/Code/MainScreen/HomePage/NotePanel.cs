using System;
using UnityEngine;

public class NotePanel : MonoBehaviour, IScreenElement
{
    [SerializeField] private BottomPanel bottomPanel;
    [SerializeField] private ScalableButton cancelButton;
    [SerializeField] private ScalableButton doneButton;
    [SerializeField] private ScalableButton deleteButton;
    [SerializeField] private TMPro.TMP_InputField inputField;

    public event Action OnDone;
    public event Action OnCancel;
    public event Action OnDelete;

    public event Action OnClose;

    public string NoteText => inputField.text;


    private void Awake()
    {
        cancelButton.OnClick.AddListener(() => Cancel());
        doneButton.OnClick.AddListener(() => Done());
        deleteButton.OnClick.AddListener(() => Delete());
    }

    private void Cancel()
    {
        OnCancel?.Invoke();
        inputField.text = "";
        Hide(false);
    }

    private void Done()
    {
        OnDone?.Invoke();
        Hide(false);
    }

    private void Delete()
    {
        OnDelete?.Invoke();
        inputField.text = "";
        Hide(false);
    }

    public void SetText(string text)
    {
        inputField.text = text;
    }

    public void SetDeleteButtonActive(bool active)
    {
        deleteButton.gameObject.SetActive(active);
        cancelButton.gameObject.SetActive(!active);
    }

    public void Show(bool immediately = false)
    {
        bottomPanel.Show(immediately);
    }

    public void Hide(bool immediately = false)
    {
        bottomPanel.Hide(immediately);
        OnClose?.Invoke();
    }
}

