using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshChase", menuName = "UnitState/NavMeshChase")]
public class NavMeshChase : UnitState
{
    private NavMeshAgent _agent;
    private bool _targetIsEnemy;
    private MapInfo _mapInfo;
    private Unit _targetUnit;
    private float _startAttackDistance = 0;
    
    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;
        
        _agent = _unit.GetComponent<NavMeshAgent>();
        if(_agent == null) Debug.LogError($"Character {unit.name} has no component NavMeshAgent");
        _mapInfo = MapInfo.Instance;
    }
    
    public override void Init()
    {
        if (_mapInfo.TryGetNearestUnit(_unit.transform.position, _targetIsEnemy, out _targetUnit, out float distance))
        {
            _startAttackDistance = _unit.Parameters.StartAttackDistance + _targetUnit.Parameters.modelRadius;
        }
    }

    public override void Run()
    {
        if (_targetUnit == null)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }

        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);

        if (distanceToTarget > _unit.Parameters.stopChaseDistance) _unit.SetState(UnitStateType.Default);
        else if (distanceToTarget <= _startAttackDistance) _unit.SetState(UnitStateType.Attack);
        else _agent.SetDestination(_targetUnit.transform.position);
    }

    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }
    
#if UNITY_EDITOR
    public override void DebugDrawDistance(Unit unit)
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.startChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.Parameters.stopChaseDistance);
    }
#endif
}