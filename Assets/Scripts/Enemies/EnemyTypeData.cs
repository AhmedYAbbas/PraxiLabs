using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy/Enemy Type", order = 0)]
public class EnemyTypeData : ScriptableObject
{
    [Header("Identity")]
    [field:SerializeField] public string Name { get; set; }

    [field: Space, Header("Stats")]
    [field:SerializeField] public float MoveSpeed { get; set; } = 1.5f;
    [field:SerializeField] public float WanderRadius { get; set; } = 8f;
    [field:SerializeField] public float ChangeTargetTime { get; set; } = 3f;

    [field: Space, Header("Visual")]
    [field:SerializeField] public Enemy Prefab { get; set; }

    public WaveFactory WaveFactory { get; set; }

    public Enemy Create()
    {
        var enemy = Instantiate(Prefab);
        enemy.gameObject.SetActive(false);
        enemy.InitializePoolElement(this, Name, MoveSpeed, WanderRadius, ChangeTargetTime);

        return enemy;
    }

    public void OnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.Spawn(WaveFactory.transform.position, WaveFactory.transform.rotation);
    }
    public void OnRelease(Enemy enemy)
    {
        //enemy.Die();
        enemy.gameObject.SetActive(false);
    }

    public void OnDestroyPoolObject(Enemy enemy) => Destroy(enemy.gameObject);
}
