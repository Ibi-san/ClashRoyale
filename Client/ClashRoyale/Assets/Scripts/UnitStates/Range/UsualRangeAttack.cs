using UnityEngine;

[CreateAssetMenu(fileName = "_UsualRangeAttack", menuName = "UnitState/UsualRangeAttack")]
public class UsualRangeAttack : UnitStateAttack
{
    [SerializeField] private Arrow _arrow;

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

    protected override void Attack()
    {
        Vector3 unitPosition = _unit.transform.position;
        Vector3 targetPosition = _target.transform.position;
        
        Arrow arrow = Instantiate(_arrow, unitPosition, Quaternion.identity);
        arrow.Init(targetPosition);
        
        float delay = Vector3.Distance(unitPosition, targetPosition) / arrow.speed;
        _target.ApplyDelayDamage(delay, _damage);
    }
}