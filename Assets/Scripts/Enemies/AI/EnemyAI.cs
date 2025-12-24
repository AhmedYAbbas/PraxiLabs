using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy Enemy => _enemy;

    private IEnemyState _currentState;
    private Enemy _enemy;

    private void Update() => _currentState?.Tick();

    public void Initialize(Enemy enemy) => _enemy = enemy;

    public void ChangeState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
