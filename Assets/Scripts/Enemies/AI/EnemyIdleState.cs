using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float _idleDuration;
    private float _timer;
    private Animator _animator;

    public EnemyIdleState(EnemyAI ai, float duration)
        : base(ai)
    {
        _idleDuration = duration;
        _animator = _ai.Enemy.EnemyAnimator;
    }

    public override void Enter()
    {
        _animator.SetBool("Wander", false);
        _timer = _idleDuration;
        _movement.ClearTarget();
    }

    public override void Tick()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _ai.ChangeState(new EnemyWanderState(_ai, _ai.Enemy.Movement.WanderRadius, _ai.Enemy.Movement.ChangeTargetTime));
        }
    }
}
