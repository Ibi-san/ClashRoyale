using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _fillHealthImage;
    private float _maxHealth;

    private void Start()
    {
        _maxHealth = _unit.health.max;
        _unit.health.UpdateHealth += UpdateHealth;
        _healthBar.SetActive(false);
    }

    private void UpdateHealth(float currentValue)
    {
        _healthBar.SetActive(true);
        _fillHealthImage.fillAmount = currentValue / _maxHealth;
    }

    private void OnDestroy()
    {
        _unit.health.UpdateHealth -= UpdateHealth;
    }
}