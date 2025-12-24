using UnityEngine;

public abstract class EnemyState : IEnemyState
{
    protected readonly EnemyAI _ai;
    protected readonly EnemyMovement _movement;

    protected EnemyState(EnemyAI ai)
    {
        _ai = ai;
        _movement = ai.Enemy.Movement;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
}
