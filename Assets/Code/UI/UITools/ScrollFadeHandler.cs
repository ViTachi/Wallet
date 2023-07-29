using UnityEngine;
using UnityEngine.UI;

public class ScrollFadeHandler : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _contentRT;
    [SerializeField] private RectTransform _topFaderRT;
    [SerializeField] private RectTransform _bottomFaderRT;
    [SerializeField] private RectTransform _leftFaderRT;
    [SerializeField] private RectTransform _rightFaderRT;

    private float _topFaderSize;
    private float _bottomFaderSize;
    private float _leftFaderSize;
    private float _rightFaderSize;

    private RectTransform _scrollRectRT;

    private void Awake()
    {
        _scrollRect.onValueChanged.AddListener(RecalculateFadersSize);

        _scrollRectRT = _scrollRect.transform as RectTransform;
        _topFaderSize = _topFaderRT == null ? 0 : _topFaderRT.rect.height;
        _bottomFaderSize = _bottomFaderRT == null ? 0 : _bottomFaderRT.rect.height;
        _leftFaderSize = _leftFaderRT == null ? 0 : _leftFaderRT.rect.width;
        _rightFaderSize = _rightFaderRT == null ? 0 : _rightFaderRT.rect.width;

        RecalculateFadersSize(Vector2.zero);
    }

    public void RecalculateFadersSize(Vector2 amount)
    {
        if(_topFaderRT != null)
        {
            _topFaderRT.sizeDelta = new Vector2(_topFaderRT.sizeDelta.x, Mathf.Clamp(_contentRT.anchoredPosition.y, 0, _topFaderSize));
        }
        if (_bottomFaderRT != null)
        {
            _bottomFaderRT.sizeDelta = new Vector2(_bottomFaderRT.sizeDelta.x, Mathf.Clamp(
                _contentRT.rect.size.y - _scrollRectRT.rect.size.y - _contentRT.anchoredPosition.y, 0, _bottomFaderSize));
        }
        if (_leftFaderRT != null)
        {
            _leftFaderRT.sizeDelta = new Vector2(Mathf.Clamp(-_contentRT.anchoredPosition.x, 0, _leftFaderSize), _leftFaderRT.sizeDelta.y);
        }
        if (_rightFaderRT != null)
        {
            _rightFaderRT.sizeDelta = new Vector2(Mathf.Clamp(
                _contentRT.rect.size.x - _scrollRectRT.rect.size.x + _contentRT.anchoredPosition.x, 0, _rightFaderSize), _rightFaderRT.sizeDelta.y);
        }
    }
}
