using UnityEngine;

[CreateAssetMenu(fileName = "_UsualRangeAttack", menuName = "UnitState/UsualRangeAttack")]
public class UsualRangeAttack : UnitStateAttack
{
    protected override bool TryFindTarget(out float stopAttackDistance)
    {
        Vector3 unitPosition = _unit.transform.position;

        bool hasEnemy = _mapInfo.TryGetNearestAnyUnit(unitPosition, _targetIsEnemy, out Unit enemy, out float distance);
        if (hasEnemy && distance - enemy.parameters.modelRadius <= _unit.parameters.StartAttackDistance)
        {
            _target = enemy.health;
            stopAttackDistance = _unit.parameters.StopAttackDistance + enemy.parameters.modelRadius;
            return true;
        }

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
