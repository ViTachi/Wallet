using System;
using System.Collections.Generic;

public class UIScreens : Singleton<UIScreens>
{
    private Dictionary<Type, IScreenElement> screenElements;

    public T GetElement<T>() where T : IScreenElement
    {
        // for (int i = 0; i < screenElements.Length; i++)
        // {
        //     if (screenElements[i] is T) return (T)screenElements[i];
        // }
        screenElements.TryGetValue(typeof(T), out IScreenElement element);
        return element == default ? default : (T)element;
    }

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
