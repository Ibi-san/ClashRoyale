using UnityEngine;

[CreateAssetMenu(fileName = "_NavMeshSiegeMove", menuName = "UnitState/NavMeshSiegeMove")]
public class NavMeshSiegeMove : UnitStateNavMeshMove
{
    protected override bool TryFindTarget(out UnitStateType changeType)
    {
        float distanceToTarget = _nearestTower.GetDistance(_unit.transform.position);
        if (distanceToTarget <= _unit.parameters.StartAttackDistance)
        {
            changeType = UnitStateType.Attack;
            return true;
        }
        else
        {
            changeType = UnitStateType.None;
            return false;
        }
    }
}
