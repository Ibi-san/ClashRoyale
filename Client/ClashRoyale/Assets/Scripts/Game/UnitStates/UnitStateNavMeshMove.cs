using UnityEngine;
using UnityEngine.AI;

public abstract class UnitStateNavMeshMove : UnitState
{
    private NavMeshAgent _agent;
    protected bool _targetIsEnemy;
    protected Tower _nearestTower;
    protected MapInfo _mapInfo;

    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;

        _agent = _unit.GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.LogError($"Character {unit.name} has no component NavMeshAgent");
        _agent.speed = _unit.parameters.speed;
        _agent.radius = _unit.parameters.modelRadius;
        _agent.stoppingDistance = _unit.parameters.StartAttackDistance;

        _mapInfo = MapInfo.Instance;
    }

    public override void Init()
    {
        Vector3 unitPosition = _unit.transform.position;
        _nearestTower = _mapInfo.GetNearestTower(in unitPosition, _targetIsEnemy);
        _agent.SetDestination(_nearestTower.transform.position);
    }

    public override void Run()
    {
        if (TryFindTarget(out UnitStateType changeType))
            _unit.SetState(changeType);
    }

    public override void Finish() => _agent.SetDestination(_unit.transform.position);

    protected abstract bool TryFindTarget(out UnitStateType changeType);
}