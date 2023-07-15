using System;
using System.Collections.Generic;

public class UIScreens : Singleton<UIScreens>
{
    private Dictionary<Type, IScreenElement> screenElements;

    public T GetElement<T>() where T : IScreenElement
    {
        screenElements.TryGetValue(typeof(T), out IScreenElement element);
        return element == default ? default : (T)element;
    }

    public void ShowElement<T>(bool immediately = false) where T : IScreenElement => GetElement<T>().Show(immediately);
    public void HideElement<T>(bool immediately = false) where T : IScreenElement => GetElement<T>().Hide(immediately);

    public void HideAllElements(bool immediately)
    {
        foreach (var element in screenElements)
        {
            element.Value.Hide(immediately);
        }
    }

    protected override void Setup()
    {
        screenElements = new Dictionary<Type, IScreenElement>();
        foreach (var element in GetComponentsInChildren<IScreenElement>(true))
        {
            screenElements.Add(element.GetType(), element);
        }
        HideAllElements(true);
    }
}
