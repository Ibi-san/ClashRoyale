using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action Death;
    [SerializeField] private HealthUI _ui;
    [field: SerializeField] public float max { get; private set; } = 10f;
    private float _current;

    private void Start()
    {
        _current = max;
    }

    public void ApplyDamage(float value)
    {
        _current -= value;
        if (_current < 0)
        {
            _current = 0;
            Death?.Invoke();
        }
        UpdateHP();
    }

    private void UpdateHP()
    {
        _ui.UpdateHealth(max, _current);
    }
}