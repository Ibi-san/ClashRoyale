using UnityEngine;

[CreateAssetMenu(fileName = "Empty", menuName = "UnitState/Empty")]
public class EmptyUnitState : UnitState
{
    public override void Init()
    {
        
    }

    public override void Run()
    {
        _unit.SetState(UnitStateType.Default);
    }

    public override void Finish()
    {
        Debug.LogWarning($"Unit {_unit.name} was in empty state, he was moved to default state");
    }
}