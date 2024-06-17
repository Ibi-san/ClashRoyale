using UnityEngine;

[CreateAssetMenu(fileName = "_NavMeshMeleeMove", menuName = "UnitState/NavMeshMeleeMove")]
public class NavMeshMeleeMove : UnitStateNavMeshMove
{
    protected override bool TryFindTarget(out UnitStateType changeType)
    {
        changeType = UnitStateType.None;
        
        if (TryAttackTower())
        {
            changeType = UnitStateType.Attack;
            return true;
        }

        if (TryChaseUnit())
        {
            changeType = UnitStateType.Chase;
            return true;
        }

        return false;
    }
    
    private bool TryAttackTower()
    {
        float distanceToTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceToTarget <= _unit.parameters.StartAttackDistance)
        {
            return true;
        }

        return false;
    }

    private bool TryChaseUnit()
    {
        bool hasEnemy = _mapInfo.TryGetNearestWalkUnit(_unit.transform.position, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy == false) return false;

        if (_unit.parameters.startChaseDistance >= distance)
        {
            return true;
        }

        return false;
    }
}