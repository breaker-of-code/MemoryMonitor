using UnityEngine;
using UnityEngine.Profiling;
using TMPro;
using System.Collections.Generic;
using Unity.Mathematics;

public enum Edge
{
    None,
    Top,
    Bottom,
    Left,
    Right
}

public class MemoryMonitor : MonoBehaviour
{
    public static MemoryMonitor SInstance;

    [SerializeField] private TMP_Text _memoryDisplay;

    [Space] [SerializeField] private float _refreshInterval = 1f;
    private float _timeSinceLastUpdate = 0f;

    private Dictionary<string, GameObject> _addressableMemoryUsage = new();

    [Space] [SerializeField] private RectTransform _closeButton;
    [SerializeField] private RectTransform _openButton;
    [SerializeField] private RectTransform _bgClose;

    private void Awake()
    {
        if (SInstance == null)
            SInstance = this;
    }

    private void Start()
    {
        UpdateMemoryDisplay();
    }

    private void Update()
    {
        _timeSinceLastUpdate += Time.deltaTime;

        if (_timeSinceLastUpdate >= _refreshInterval)
        {
            UpdateMemoryDisplay();
            _timeSinceLastUpdate = 0f;
        }
    }

    private void UpdateMemoryDisplay()
    {
        long totalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024);
        long totalReservedMemory = Profiler.GetTotalReservedMemoryLong() / (1024 * 1024);
        long totalUnusedReservedMemory = Profiler.GetTotalUnusedReservedMemoryLong() / (1024 * 1024);

        _memoryDisplay.text = $"Total Memory:{totalReservedMemory}MB\n" +
                              $"Used Memory:{totalAllocatedMemory}MB\n" +
                              $"Available Memory:{totalUnusedReservedMemory}MB";
    }

    public void UpdateButtonArrow(Edge edge)
    {
        if (_openButton is null || _closeButton is null || _bgClose is null) return;

        if (edge == Edge.Left)
        {
            _closeButton.localEulerAngles = new Vector3(0, 0, 180.0f);
            _openButton.localEulerAngles = Vector3.zero;

            _bgClose.anchoredPosition = new Vector2(math.abs(_bgClose.anchoredPosition.x) * -1.0f,
                -_bgClose.anchoredPosition.y);
        }
        else if (edge == Edge.Right)
        {
            _closeButton.localEulerAngles = Vector3.zero;
            _openButton.localEulerAngles = new Vector3(0, 0, 180.0f);

            _bgClose.anchoredPosition =
                new Vector2(math.abs(_bgClose.anchoredPosition.x), -_bgClose.anchoredPosition.y);
        }
        else if (edge == Edge.Bottom)
        {
            _closeButton.localEulerAngles = new Vector3(0, 0, -90.0f);
            _openButton.localEulerAngles = new Vector3(0, 0, 90.0f);

            _bgClose.anchoredPosition =
                new Vector2(math.abs(_bgClose.anchoredPosition.x), -_bgClose.anchoredPosition.y);
        }
        // top
        else
        {
            _closeButton.localEulerAngles = new Vector3(0, 0, 90.0f);
            _openButton.localEulerAngles = new Vector3(0, 0, -90.0f);

            _bgClose.anchoredPosition =
                new Vector2(math.abs(_bgClose.anchoredPosition.x), -_bgClose.anchoredPosition.y);
        }
    }
}