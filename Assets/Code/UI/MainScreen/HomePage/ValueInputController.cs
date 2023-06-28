using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueInputController : MonoBehaviour
{
    [SerializeField] private int maxFractionalNumber = 2;
    [SerializeField] private int maxWholeNumbers = 10;
    [SerializeField] private Color activeFractionalColor = Color.white;
    [SerializeField] private Color disableFractionalColor = Color.white;
    [SerializeField] private TMPro.TMP_Text wholeValueText;
    [SerializeField] private TMPro.TMP_Text fractionalValueText;
    [SerializeField] private List<NumberInputButton> numberButtons;
    [SerializeField] private Button dotButton;
    [SerializeField] private Button backspaceButton;

    public event Action<float> OnChangeValue;
    public float CurrentVulue => CalculateValue();

    private int wholeValue;
    private int fractionalValue;

    private bool isFractionalInput = false;

    private int fractionalNumbersCount = 0;

    private void Awake()
    {
        dotButton.onClick.AddListener(OnDotButtonClicked);
        backspaceButton.onClick.AddListener(OnBackspaceButtonClicked);
        foreach (var numberButton in numberButtons)
        {
            numberButton.Button.onClick.AddListener(() => OnNumberButtonClicked(numberButton.Number));
        }

        ResetContoller();
    }

    public void ResetContoller()
    {
        wholeValueText.text = "0";
        wholeValue = 0;

        string text = "";
        for (int i = 0; i < maxFractionalNumber; i++)
        {
            text = $"{text}0";
        }
        fractionalValueText.text = text;

        fractionalValue = 0;
        fractionalNumbersCount = 0;
        SetFractionalActive(false);

        OnChangeValue?.Invoke(CurrentVulue);
    }

    private void OnNumberButtonClicked(int number)
    {
        if (isFractionalInput)
        {
            if (fractionalNumbersCount < maxFractionalNumber)
            {
                string text = fractionalNumbersCount == 0 ? number.ToString() : $"{fractionalValue}{number}";
                bool parseComplete = int.TryParse(text, out int resault);
                if (parseComplete) fractionalValue = resault;
                fractionalNumbersCount++;

                for (int i = fractionalNumbersCount; i < maxFractionalNumber; i++)
                {
                    text = $"{text}0";
                }           
               
                fractionalValueText.text = text;
            }
        }
        else
        {
            if (wholeValueText.text.Length < maxWholeNumbers)
            {
                wholeValueText.text = wholeValue == 0 ? number.ToString() : $"{wholeValue}{number}";
                bool parseComplete = int.TryParse(wholeValueText.text, out int resault);
                if (parseComplete) wholeValue = resault;
            }
        }

        OnChangeValue?.Invoke(CurrentVulue);
    }

    private void OnDotButtonClicked()
    {
        SetFractionalActive(true);
    }

    private void OnBackspaceButtonClicked()
    {
        if (isFractionalInput)
        {
            if (fractionalNumbersCount == 0)
            {
                SetFractionalActive(false);
                OnBackspaceButtonClicked();
            }
            string text = "";
            if (fractionalNumbersCount > 1)
            {
                text = fractionalValueText.text.Remove(fractionalNumbersCount - 1, maxFractionalNumber - fractionalNumbersCount + 1);
                bool parseComplete = int.TryParse(text, out int resault);
                if (parseComplete) fractionalValue = resault;
                fractionalNumbersCount--;
            }
            else
            {
                fractionalValue = 0;
                fractionalNumbersCount = 0;
            }
           
            for (int i = fractionalNumbersCount; i < maxFractionalNumber; i++)
            {
                text = $"{text}0";
            }
            fractionalValueText.text = text;
        }
        else
        {
            wholeValueText.text = wholeValueText.text.Length <= 1 ? "0" : wholeValueText.text.Remove(wholeValueText.text.Length - 1);
            bool parseComplete = int.TryParse(wholeValueText.text, out int resault);
            if (parseComplete) wholeValue = resault;
        }

        OnChangeValue?.Invoke(CurrentVulue);
    }

    private void SetFractionalActive(bool active)
    {
        isFractionalInput = active;
        fractionalValueText.color = active ? activeFractionalColor : disableFractionalColor;
    }

    private float CalculateValue()
    {
        return float.Parse($"{wholeValue},{fractionalValue}");
    }
}
