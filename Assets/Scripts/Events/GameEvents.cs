using System;

public static class GameEvents
{
    public static Action<int> OnWaveStarted;
    public static Action OnWaveCompleted;
    public static Action<int, int> OnEnemyCountChanged;
    public static Action<bool> OnGamePaused;

    public static Action<int> OnNextWaveDelayTick;
    public static Action OnNextWaveDelayFinished;
}
