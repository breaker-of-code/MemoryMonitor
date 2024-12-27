using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEdgeMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Settings")] public bool _snapToEdges = true;

    private Edge _snappedEdge = Edge.None;

    [Space] [SerializeField] private RectTransform _rectTransform;

    private bool _isDragging;

    void Awake()
    {
        _rectTransform ??= GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;

        if (_snapToEdges)
        {
            SnapToEdge();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDragging)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint
            );
            _rectTransform.localPosition = localPoint;
        }
    }

    private void SnapToEdge()
    {
        RectTransform parentRect = _rectTransform.parent as RectTransform;
        Vector3 position = _rectTransform.localPosition;

        float halfWidth = _rectTransform.rect.width / 2;
        float halfHeight = _rectTransform.rect.height / 2;

        float left = -parentRect.rect.width / 2 + halfWidth;
        float right = parentRect.rect.width / 2 - halfWidth;
        float top = parentRect.rect.height / 2 - halfHeight;
        float bottom = -parentRect.rect.height / 2 + halfHeight;

        float closestX = (Mathf.Abs(position.x - left) < Mathf.Abs(position.x - right)) ? left : right;
        float closestY = (Mathf.Abs(position.y - bottom) < Mathf.Abs(position.y - top)) ? bottom : top;

        if (Mathf.Abs(position.x - closestX) < Mathf.Abs(position.y - closestY))
        {
            position.x = closestX;
            _snappedEdge = (closestX == left) ? Edge.Left : Edge.Right;
        }
        else
        {
            position.y = closestY;
            _snappedEdge = (closestY == top) ? Edge.Top : Edge.Bottom;
        }

        _rectTransform.localPosition = position;

        MemoryMonitor.SInstance.UpdateButtonArrow(_snappedEdge);
    }
}