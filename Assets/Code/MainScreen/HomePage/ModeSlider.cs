using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModeSlider : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Ease moveEase;

    [SerializeField] private RectTransform backgroundRT;

    [SerializeField] private RectTransform carretRT;
    [SerializeField] private RectTransform carretInputArea;
    [SerializeField] private InputHandler carretInputHandler;
    [SerializeField] private float slideVelocityToChangeMode;
    [SerializeField] private List<Button> modeButtons;

    [Header("Debug")]
    [SerializeField] private TMPro.TMP_Dropdown easeDropdown;
    [SerializeField] private TMPro.TMP_InputField moveSpeedInputField;

    public event Action<int> OnModeChanged;

    private int currentModeNumber;
    public int CurrentModeNumber => currentModeNumber;

    private void Awake()
    {
        for (int i = 0; i < modeButtons.Count; i++)
        {
            int number = i + 1;
            modeButtons[i].onClick.AddListener(() => OnModeButtonClicked(number));
        }

        carretInputHandler.OnBeginDragEvent += OnCarretBeginDrag;
        carretInputHandler.OnDragEvent += OnCarretDrag;
        carretInputHandler.OnEndDragEvent += OnCarretEndDrag;
        carretInputHandler.OnPointerUpEvent += OnCarretRelease;

        string[] values = Enum.GetNames(typeof(Ease));
        easeDropdown.AddOptions(values.ToList());
        easeDropdown.onValueChanged.AddListener((value) => { moveEase = (Ease)value; });
        moveSpeedInputField.onEndEdit.AddListener((value) =>
        {
            bool parse = float.TryParse(value, out float resault);
            if (parse) moveSpeed = resault;
        });
    }  

    public void SetModeVisibility(int modeNumber, bool visibility)
    {
        if (modeButtons.Count < 1) return;

        modeButtons[Mathf.Clamp(modeNumber, 1, modeButtons.Count) - 1].gameObject.SetActive(visibility);

        UpdateCarretWidth();
    }

    private void UpdateCarretWidth()
    {
        if (modeButtons.Count < 1 || modeButtons.Where(x => x.gameObject.activeSelf).Count() < 1) return;

        LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundRT);
        Vector2 newSize = new Vector2(modeButtons.Where(x => x.gameObject.activeSelf).First().targetGraphic.rectTransform.sizeDelta.x, carretRT.sizeDelta.y);
        carretRT.sizeDelta = newSize;
        carretInputArea.sizeDelta = newSize;
        currentModeNumber = Mathf.Clamp(currentModeNumber, 1, modeButtons.Count);
        if (modeButtons[currentModeNumber - 1].gameObject.activeSelf) MoveCurretToButton(modeButtons[currentModeNumber - 1]);
        else
        {
            SetMode(modeButtons.IndexOf(modeButtons.Where(x => x.gameObject.activeSelf).First()) + 1);
        }
    }

    private void OnModeButtonClicked(int modeNumber)
    {
        SetMode(modeNumber);
    }

    public void SetMode(int modeNumber, bool immidiately = false)
    {
        if (modeButtons.Count < 1) return;
        modeNumber = Mathf.Clamp(modeNumber, 1, modeButtons.Count);
        currentModeNumber = modeNumber;
        OnModeChanged?.Invoke(currentModeNumber);

        MoveCurretToButton(modeButtons[modeNumber - 1], immidiately);
    }

    private void MoveCurretToButton(Button button, bool immidiately = false)
    {
        DOTween.Kill(this);
        if (button == null) return;

        if (immidiately)
        {
            carretRT.transform.position = button.transform.position;
        }
        else
        {
            carretRT.DOMove(button.transform.position, moveSpeed)
                .SetUpdate(true)
                .SetId(this)
                .SetSpeedBased()
                .SetEase(moveEase);
        }
    }

    private void OnCarretBeginDrag(PointerEventData eventData)
    {
        lastCarretVelocity = 0;
        DOTween.Kill(this);
    }

    private void OnCarretDrag(PointerEventData eventData)
    {
        MoveCarret(eventData.delta.x);
        //lastCarretVelocity = eventData.delta.x;
    }

    private void OnCarretEndDrag(PointerEventData eventData)
    {
        lastCarretVelocity = 0;
    }

    private float lastCarretVelocity = 0;

    private void OnCarretRelease(PointerEventData eventData)
    {
        DOTween.Kill(this);

        if (modeButtons.Count < 1 || modeButtons.Where(x => x.gameObject.activeSelf).Count() < 1) return;

        Button targetButton = null;

        int buttonIndex = 0;

        if (Mathf.Abs(lastCarretVelocity) >= slideVelocityToChangeMode)
        {
            Button currentClosestButton = modeButtons.Where(x => x.gameObject.activeSelf).OrderBy(x => (x.transform.position - carretRT.position).sqrMagnitude).FirstOrDefault();

            buttonIndex = modeButtons.IndexOf(currentClosestButton) + Mathf.RoundToInt(Mathf.Sign(lastCarretVelocity));
        }
        else
        {
            targetButton = modeButtons.Where(x => x.gameObject.activeSelf).OrderBy(x => (x.transform.position - carretRT.position).sqrMagnitude).FirstOrDefault();
            buttonIndex = modeButtons.IndexOf(targetButton);
        }

        if (targetButton != null)
        {
            SetMode(buttonIndex + 1);
        }
    }

    private void MoveCarret(float delta)
    {
        if (modeButtons.Count < 1) return;

        Button lastButton = modeButtons.Where(x => x.gameObject.activeSelf).LastOrDefault();
        if(lastButton != default)
        {
            Vector2 newPosition = carretRT.position + Vector3.right * delta;
            newPosition.x = Mathf.Clamp(newPosition.x, modeButtons[0].transform.position.x,
                lastButton.transform.position.x);
            carretRT.position = newPosition;
        } 
    }


    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
