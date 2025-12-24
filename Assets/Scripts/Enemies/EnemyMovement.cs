using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed => _speed;
    public float WanderRadius => _wanderRadius;
    public float ChangeTargetTime => _changeTargetTime;

    private float _speed;
    private float _wanderRadius;
    private float _changeTargetTime;

    private Vector3 _currentTarget;
    private bool _hasTarget;

    private Rigidbody _rb;

    public void ClearTarget() => _hasTarget = false;

    public void Configure(Rigidbody rb, float moveSpeed, float wanderRadius, float changeTargetTime)
    {
        _rb = rb;
        _speed = moveSpeed;
        _wanderRadius = wanderRadius;
        _changeTargetTime = changeTargetTime;
    }

    public void SetTarget(Vector3 target)
    {
        _currentTarget = target;
        _hasTarget = true;
    }

    public bool ReachedTarget(float threshold = 0.1f)
    {
        return !_hasTarget || Vector3.Distance(transform.parent.position, _currentTarget) <= threshold;
    }

    private void FixedUpdate()
    {
        if (!_hasTarget)
            return;

        Vector3 newPos = Vector3.MoveTowards(_rb.position, _currentTarget, _speed * Time.fixedDeltaTime);
        Quaternion rotation = Quaternion.Lerp(_rb.rotation, Quaternion.LookRotation(_currentTarget - transform.parent.position), Time.fixedDeltaTime);
        rotation.x = 0f;
        rotation.z = 0f;

        _rb.Move(newPos, rotation);
    }
}
