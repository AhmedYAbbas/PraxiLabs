using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WaveFactory : MonoBehaviour
{
    [Space, Header("Pool Settings")]
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxPoolSize = 100;

    private Dictionary<EnemyTypeData, IObjectPool<Enemy>> _wavePools = new();

    public Enemy Spawn(EnemyTypeData enemyTypeData) => GetPoolFor(enemyTypeData)?.Get();
    public void ReturnToPool(Enemy enemy) => GetPoolFor(enemy.EnemyTypeData)?.Release(enemy);

    private IObjectPool<Enemy> GetPoolFor(EnemyTypeData enemyTypeData)
    {
        if (_wavePools.TryGetValue(enemyTypeData, out var pool))
            return pool;

        IObjectPool<Enemy> newPool = new ObjectPool<Enemy>(
            enemyTypeData.Create,
            enemyTypeData.OnGet,
            enemyTypeData.OnRelease,
            enemyTypeData.OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxPoolSize
        );
        enemyTypeData.WaveFactory = this;
        _wavePools.Add(enemyTypeData, newPool);

        return newPool;
    }
}
