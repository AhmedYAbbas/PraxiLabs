using TMPro;
using UnityEngine;

public class WaveHUD : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text enemyText;
    [SerializeField] TMP_Text nextWaveTimerText;

    private bool _paused = false;
    private float _delay = 5f;

    private void OnEnable()
    {
        GameEvents.OnWaveStarted += OnWaveStarted;
        GameEvents.OnEnemyCountChanged += OnEnemyCountChanged;
        GameEvents.OnWaveCompleted += OnWaveCompleted;
        GameEvents.OnGamePaused += OnGamePaused;

        GameEvents.OnNextWaveDelayTick += OnDelayTick;
        GameEvents.OnNextWaveDelayFinished += OnDelayFinished;
    }

    private void OnDisable()
    {
        GameEvents.OnWaveStarted -= OnWaveStarted;
        GameEvents.OnEnemyCountChanged -= OnEnemyCountChanged;
        GameEvents.OnWaveCompleted -= OnWaveCompleted;
        GameEvents.OnGamePaused -= OnGamePaused;

        GameEvents.OnNextWaveDelayTick += OnDelayTick;
        GameEvents.OnNextWaveDelayFinished += OnDelayFinished;
    }

    private void Update() => HandleWaveDelayText();

    private void OnEnemyCountChanged(int count, int total) => enemyText.SetText("Enemies: {0}/{1}", count, total);
    private void OnGamePaused(bool paused) => _paused = paused;
    private void OnWaveCompleted() => nextWaveTimerText.gameObject.SetActive(true);
    private void OnDelayTick(int delay) => _delay = delay;
    private void OnDelayFinished() => nextWaveTimerText.gameObject.SetActive(false);

    private void OnWaveStarted(int wave)
    {
        nextWaveTimerText.gameObject.SetActive(false);
        waveText.SetText("Wave: {0}", wave);
    }

    private void HandleWaveDelayText()
    {
        if (_paused)
            return;

        if (nextWaveTimerText.gameObject.activeSelf)
        {
            nextWaveTimerText.SetText("Next Wave In: {0}", Mathf.CeilToInt(_delay));
            nextWaveTimerText.transform.localScale = Vector3.one * (1.1f + Mathf.Sin(Time.time * 4f) * 0.05f);
        }
    }
}
