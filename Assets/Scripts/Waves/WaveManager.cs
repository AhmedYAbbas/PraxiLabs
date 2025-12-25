using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;

public class WaveManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button nextWaveButton;
    [SerializeField] private Button destroyWaveButton;

    [Header("Spawning")]
    [SerializeField] private EnemyTypeData[] enemyTypes;
    [SerializeField] private WaveFactory waveFactory;
    [SerializeField] private float spawnInterval = 0.25f;
    [SerializeField] private float delayBetweenWaves = 5f;

    public static Action<int> OnWaveStarted;
    public static Action OnWaveCompleted;
    public static Action<int, int> OnEnemyCountChanged;
    public static Action<bool> OnGamePaused;

    public static Action<int> OnNextWaveDelayTick;
    public static Action OnNextWaveDelayFinished;

    private int _currentWave;
    private int _totalEnemies;
    private int _aliveEnemies;
    private bool _waveCompletedWhilePaused = false;
    private bool _paused = false;
    private bool _transition = false;

    private readonly List<Enemy> _activeEnemies = new();

    private Coroutine _spawnCoroutine;
    private Coroutine _delayCoroutine;

    #region Unity Callbacks

    private void OnEnable()
    {
        pauseButton.onClick.AddListener(TogglePause);
        nextWaveButton.onClick.AddListener(ForceNextWave);
        destroyWaveButton.onClick.AddListener(DestroyWaveWithDelay);
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveListener(TogglePause);
        nextWaveButton.onClick.RemoveListener(ForceNextWave);
        destroyWaveButton.onClick.RemoveListener(DestroyWaveWithDelay);
    }

    #endregion

    #region Wave Flow

    private void StartNextWave()
    {
        _transition = false;

        CleanupWave();

        _currentWave++;
        _totalEnemies = GetEnemyCount(_currentWave);

        OnWaveStarted?.Invoke(_currentWave);

        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < _totalEnemies; i++)
        {
            SpawnEnemy();
            yield return Helpers.GetWaitForSeconds(spawnInterval);
        }

        TryCompleteWave();
    }

    private void SpawnEnemy()
    {
        EnemyTypeData type = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];

        Enemy enemy = waveFactory.Spawn(type);
        _activeEnemies.Add(enemy);
        _aliveEnemies++;

        OnEnemyCountChanged(_aliveEnemies, GetEnemyCount(_currentWave));
    }

    #endregion

    #region Completion Logic

    private void TryCompleteWave()
    {
        if (_paused)
        {
            _waveCompletedWhilePaused = true;
            return;
        }

        if (_delayCoroutine != null)
            return;

        OnWaveCompleted?.Invoke();
        _delayCoroutine = StartCoroutine(NextWaveDelay());
    }

    private IEnumerator NextWaveDelay()
    {
        _transition = true;
        if (_paused)
        {
            _waveCompletedWhilePaused = true;
            yield break;
        }

        float remaining = delayBetweenWaves;
        int lastWholeSecond = Mathf.CeilToInt(remaining);

        OnNextWaveDelayTick?.Invoke(lastWholeSecond);

        while (remaining > 0f)
        {
            while (_paused)
                yield return null;

            remaining -= Time.deltaTime;
            int currentWholeSecond = Mathf.CeilToInt(remaining);

            if (currentWholeSecond != lastWholeSecond)
            {
                lastWholeSecond = currentWholeSecond;
                OnNextWaveDelayTick?.Invoke(currentWholeSecond);
            }

            yield return null;
        }

        OnNextWaveDelayFinished?.Invoke();
        StartNextWave();
    }

    #endregion

    #region Controls

    private void TogglePause()
    {
        _paused = !_paused;
        OnGamePaused?.Invoke(_paused);

        if (!_paused && _waveCompletedWhilePaused)
        {
            _waveCompletedWhilePaused = false;
            TryCompleteWave();
        }
    }

    private void ForceNextWave()
    {
        StopAllCoroutines();

        if (_paused)
        {
            _waveCompletedWhilePaused = true;
            CleanupWave();
            return;
        }

        StartNextWave();
    }

    private void DestroyWaveWithDelay()
    {
        if (_transition || _aliveEnemies == 0)
            return;

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        CleanupWave();
        TryCompleteWave();
    }

    #endregion

    #region Helpers

    private void CleanupWave()
    {
        foreach (var enemy in _activeEnemies)
            waveFactory.ReturnToPool(enemy);

        _activeEnemies.Clear();

        _totalEnemies = 0;
        _aliveEnemies = 0;

        _spawnCoroutine = null;
        _delayCoroutine = null;
    }

    private int GetEnemyCount(int wave)
    {
        if (wave == 1) return 30;
        if (wave == 2) return 50;
        if (wave == 3) return 70;
        return 70 + (wave - 3) * 10;
    }

    #endregion
}
