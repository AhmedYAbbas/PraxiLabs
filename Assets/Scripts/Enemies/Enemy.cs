using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] private EnemyAI enemyAI;

    [Space, Header("Movement")]
    [SerializeField] private Rigidbody enemyRigidbody;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private EnemyMovement enemyMovement;

    public EnemyTypeData EnemyTypeData { get; private set; }
    public EnemyMovement Movement => enemyMovement;
    public Animator EnemyAnimator => enemyAnimator;

    public void InitializePoolElement(EnemyTypeData typeData, string name, float moveSpeed, float wanderRadius, float changeTargetTime)
    {
        EnemyTypeData = typeData;
        gameObject.name = name;

        enemyMovement?.Configure(enemyRigidbody, moveSpeed, wanderRadius, changeTargetTime);
        enemyAI.Initialize(this);
    }

    public void Spawn(Vector3 location, Quaternion rotation)
    {
        transform.SetPositionAndRotation(location, rotation);
        enemyAI.ChangeState(new EnemyWanderState(enemyAI, enemyMovement.WanderRadius, enemyMovement.ChangeTargetTime));
    }

    public void Die()
    {
        
    }
}
