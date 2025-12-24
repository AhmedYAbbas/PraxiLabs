using UnityEngine;

public class EnemyWanderState : EnemyState
{
    private readonly float _radius;
    private readonly float _changeTargetTime;

    private float _timer;
    private Animator _animator;

    public EnemyWanderState(EnemyAI ai, float radius, float changeTargetTime)
        : base(ai)
    {
        _radius = radius;
        _changeTargetTime = changeTargetTime;
        _animator = _ai.Enemy.EnemyAnimator;
    }

    public override void Enter()
    {
        PickNewTarget();
        _animator?.SetBool("Wander", true);
    }

    public override void Tick()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0 || _movement.ReachedTarget())
            _ai.ChangeState(new EnemyIdleState(_ai, 2f));
    }

    private void PickNewTarget()
    {
        _timer = _changeTargetTime;

        Vector3 randomTarget = new Vector3(Random.Range(-_radius, _radius), 0, Random.Range(-_radius, _radius));

        _movement.SetTarget(randomTarget);
    }
}
