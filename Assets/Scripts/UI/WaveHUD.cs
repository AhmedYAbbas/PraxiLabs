using TMPro;
using UnityEngine;

public class WaveHUD : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text enemyText;
    [SerializeField] TMP_Text nextWaveTimerText;
    [SerializeField] TMP_Text pauseText;

    private bool _paused = false;
    private float _delay = 5f;

    private void OnEnable()
    {
        WaveManager.OnWaveStarted += OnWaveStarted;
        WaveManager.OnEnemyCountChanged += OnEnemyCountChanged;
        WaveManager.OnWaveCompleted += OnWaveCompleted;
        WaveManager.OnGamePaused += OnGamePaused;

        WaveManager.OnNextWaveDelayTick += OnDelayTick;
        WaveManager.OnNextWaveDelayFinished += OnDelayFinished;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveStarted -= OnWaveStarted;
        WaveManager.OnEnemyCountChanged -= OnEnemyCountChanged;
        WaveManager.OnWaveCompleted -= OnWaveCompleted;
        WaveManager.OnGamePaused -= OnGamePaused;

        WaveManager.OnNextWaveDelayTick += OnDelayTick;
        WaveManager.OnNextWaveDelayFinished += OnDelayFinished;
    }

    private void Update() => HandleWaveDelayText();

    private void OnEnemyCountChanged(int count, int total) => enemyText?.SetText("Enemies: {0}/{1}", count, total);
    private void OnWaveCompleted() => nextWaveTimerText?.gameObject.SetActive(true);
    private void OnDelayFinished() => nextWaveTimerText?.gameObject.SetActive(false);
    private void OnDelayTick(int delay) => _delay = delay;

    private void OnGamePaused(bool paused)
    {
        _paused = paused;
        pauseText?.SetText($"Paused: {_paused}");
    }

    private void OnWaveStarted(int wave)
    {
        nextWaveTimerText?.gameObject.SetActive(false);
        waveText?.SetText("Wave: {0}", wave);
    }

    private void HandleWaveDelayText()
    {
        if (_paused)
            return;

        if (nextWaveTimerText.gameObject.activeSelf)
        {
            nextWaveTimerText?.SetText("Next Wave In: {0}", Mathf.CeilToInt(_delay));
            nextWaveTimerText.transform.localScale = Vector3.one * (1.1f + Mathf.Sin(Time.time * 4f) * 0.05f);
        }
    }
}
