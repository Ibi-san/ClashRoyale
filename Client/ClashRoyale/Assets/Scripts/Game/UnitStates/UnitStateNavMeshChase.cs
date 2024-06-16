using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitStateNavMeshChase : UnitState
{
    private NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected MapInfo _mapInfo;
    protected Unit _targetUnit;
    private float _startAttackDistance = 0;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"Character {unit.name} has no component NavMeshAgent");
        _mapInfo = MapInfo.Instance;
    }

    public override void Init()
    {
        FindTargetUnit(out _targetUnit);

        if (_targetUnit == null)
        {
            _unit.SetState(UnitStateType.Default);
            return;
        }
        _startAttackDistance = _unit.parameters.StartAttackDistance + _targetUnit.parameters.modelRadius;
    }

    public override void Run()
    {
        float distanceToTarget = Vector3.Distance(_unit.transform.position, _targetUnit.transform.position);

        if (distanceToTarget > _unit.parameters.stopChaseDistance) _unit.SetState(UnitStateType.Default);
        else if (distanceToTarget <= _startAttackDistance) _unit.SetState(UnitStateType.Attack);
        else _agent.SetDestination(_targetUnit.transform.position);
    }

    public override void Finish() => _agent.SetDestination(_unit.transform.position);

    protected abstract void FindTargetUnit(out Unit targetUnit);


#if UNITY_EDITOR


    public override void DebugDrawDistance(Unit unit)
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.startChaseDistance);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(unit.transform.position, Vector3.up, unit.parameters.stopChaseDistance);
    }

#endif
}