using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "_NavMeshMove", menuName = "UnitState/NavMeshMove")]
public class NavMeshMove : UnitState
{
    private NavMeshAgent _agent;
    private bool _targetIsEnemy;
    private Tower _nearestTower;
    private MapInfo _mapInfo;
    public override void Constructor(Unit unit)
    {
        base.Constructor(unit);

        _targetIsEnemy = _unit.isEnemy == false;
        
        _agent = _unit.GetComponent<NavMeshAgent>();
        if(_agent == null) Debug.LogError($"Character {unit.name} has no component NavMeshAgent");
        _agent.speed = _unit.Parameters.speed;
        _agent.radius = _unit.Parameters.modelRadius;
        _agent.stoppingDistance = _unit.Parameters.StartAttackDistance;
        
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
        if(TryAttackTower()) return;
        if (TryAttackUnit()) return;
    }

    private bool TryAttackTower()
    {
        float distanceToTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceToTarget <= _unit.Parameters.StartAttackDistance)
        {
            _unit.SetState(UnitStateType.Attack);
            return true;
        }

        return false;
    }

    private bool TryAttackUnit()
    {
        bool hasEnemy = _mapInfo.TryGetNearestUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy == false) return false;
        
        if (_unit.Parameters.startChaseDistance >= distance)
        {
            _unit.SetState(UnitStateType.Chase);
            return true;
        }
        return false;
    }

    public override void Finish()
    {
        _agent.SetDestination(_unit.transform.position);
    }
}
