public interface ISelectable<T>
{
    event System.Action<T> OnSelected;
    bool IsSelected { get; }
    void SetIsSelected(bool selected);
}
