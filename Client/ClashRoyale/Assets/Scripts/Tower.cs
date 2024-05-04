using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IHealth, IDestroyable
{
    [field: SerializeField] public Health health { get; private set; }
    
    [field: SerializeField] public bool isEnemy { get; private set; } = false;
    [field: SerializeField] public float radius { get; private set; } = 2f;

    public float GetDistance(in Vector3 point) => Vector3.Distance(transform.position, point) - radius;

    private void Start()
    {
        health.Death += Destroy;
    }

    public void Destroy()
    {
        MapInfo.Instance.RemoveFromList(this, isEnemy);
        health.Death -= Destroy;
        Destroy(gameObject);
    }
}
