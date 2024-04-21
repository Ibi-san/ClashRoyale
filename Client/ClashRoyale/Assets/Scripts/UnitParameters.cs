using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [field: SerializeField] public float speed { get; private set; } = 4f;
    [field: SerializeField] public float modelRadius { get; private set; } = 1f;
    [field: SerializeField] public float startChaseDistance { get; private set; } = 5f;
    [field: SerializeField] public float stopChaseDistance { get; private set; } = 7f;
    public float StartAttackDistance => _startAttackDistance + modelRadius;
    public float StopAttackDistance => _stopAttackDistance + modelRadius;
    
    [SerializeField] public float _startAttackDistance = 1f;
    [SerializeField] public float _stopAttackDistance = 1.5f;
}
