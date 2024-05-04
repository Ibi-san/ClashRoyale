using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> UpdateHealth;
    [field: SerializeField] public float max { get; private set; } = 10f;
    private float _current;

    private void Start()
    {
        _current = max;
    }

    public void ApplyDamage(float value)
    {
        _current -= value;
        if (_current < 0) _current = 0;
        
        UpdateHealth?.Invoke(_current);
    }
}

interface IHealth
{
    Health health { get; }
}