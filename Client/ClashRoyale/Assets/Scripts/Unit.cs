using System;
using UnityEngine;

[RequireComponent(typeof(UnitParameters), typeof(Health))]
public class Unit : MonoBehaviour, IHealth, IDestroyed
{
    [field: SerializeField] public Health health { get; private set; }
    [field: SerializeField] public bool isEnemy { get; private set; } = false;
    [field: SerializeField] public UnitParameters Parameters { get; private set; }
    [SerializeField] private UnitState _defaultStateSO;
    [SerializeField] private UnitState _chaseStateSO;
    [SerializeField] private UnitState _attackStateSO;
    private UnitState _defaultState;
    private UnitState _chaseState;
    private UnitState _attackState;
    private UnitState _currentState;
    public event Action Destroyed;


    private void Start()
    {
        CreateStates();

        _currentState = _defaultState;
        _currentState.Init();

        health.UpdateHealth += CheckDestroy;
    }

    private void Update()
    {
        _currentState.Run();
    }

    private void CreateStates()
    {
        _defaultState = Instantiate(_defaultStateSO);
        _defaultState.Constructor(this);

        _chaseState = Instantiate(_chaseStateSO);
        _chaseState.Constructor(this);

        _attackState = Instantiate(_attackStateSO);
        _attackState.Constructor(this);
    }

    private void CheckDestroy(float currentHP)
    {
        if (currentHP > 0) return;
        
        Destroy(gameObject);
        health.UpdateHealth -= CheckDestroy;
        Destroyed?.Invoke();
    }

    public void SetState(UnitStateType type)
    {
        _currentState.Finish();
        
        switch (type)
        {
            case UnitStateType.Default:
                _currentState = _defaultState;
                break;
            case UnitStateType.Chase:
                _currentState = _chaseState;
                break;
            case UnitStateType.Attack:
                _currentState = _attackState;
                break;
            default:
                Debug.LogError("UnitStateType not handled");
                break;
        }
        
        _currentState.Init();
    }

#if UNITY_EDITOR
    [Space(24)] [SerializeField] private bool _debug = false;
    private void OnDrawGizmos()
    {
        if (_debug == false) return;
        if (_chaseStateSO != null) _chaseStateSO.DebugDrawDistance(this);
    }
#endif
}
