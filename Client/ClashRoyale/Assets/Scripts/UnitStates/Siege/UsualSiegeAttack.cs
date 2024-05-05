using UnityEngine;

[CreateAssetMenu(fileName = "_UsualSiegeAttack", menuName = "UnitState/UsualSiegeAttack")]
public class UsualSiegeAttack : UnitStateAttack
{
    protected override bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;
        Tower targetTower = _mapInfo.GetNearestTower(unitPosition, _targetIsEnemy);
        if (targetTower.GetDistance(unitPosition) <= _unit.parameters.StartAttackDistance)
        {
            _target = targetTower.health;
            stopAttackDistance = _unit.parameters.StopAttackDistance + targetTower.radius;
            return true;
        }

        stopAttackDistance = 0;
        return false;
    }
}
