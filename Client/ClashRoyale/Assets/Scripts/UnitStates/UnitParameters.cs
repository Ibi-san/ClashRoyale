using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; private set; } = 4f;
    [field: SerializeField] public float ModelRadius { get; private set; } = 1f;
    public float StartAttackDistance => _startAttackDistance + ModelRadius;
    public float StopAttackDistance => _stopAttackDistance + ModelRadius;
    
    [SerializeField] public float _startAttackDistance = 1f;
    [SerializeField] public float _stopAttackDistance = 1.5f;
}
