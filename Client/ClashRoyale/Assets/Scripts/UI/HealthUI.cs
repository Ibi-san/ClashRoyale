using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filledImageRect;
    [SerializeField] private float _defaultWidth;
    [SerializeField] private bool _isEnemy;
    [SerializeField] private Color _enemyColor;
    [SerializeField] private Color _playerColor;
    [SerializeField] private Image _filledImageColor;
    private void OnValidate()
    {
        _defaultWidth = _filledImageRect.sizeDelta.x;
    }

    private void Start()
    {
        _filledImageColor.color = _isEnemy ? _enemyColor : _playerColor;
    }

    public void UpdateHealth(float max, float current)
    {
        float percent = current / max;
        _filledImageRect.sizeDelta = new Vector2(_defaultWidth * percent, _filledImageRect.sizeDelta.y);
    }
}
