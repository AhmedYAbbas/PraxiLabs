using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FPSDisplay : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private TMP_Text fpsText;

    [Space, Header("FPS Settings")]
    [SerializeField] private float updateInterval = 0.5f;
    [SerializeField] private Color highFPSColor = Color.green;
    [SerializeField] private Color mediumFPSColor = Color.yellow;
    [SerializeField] private Color lowFPSColor = Color.red;
    [SerializeField] private int mediumFPSThreshold = 40;
    [SerializeField] private int lowFPSThreshold = 20;

    private float _accum = 0f;
    private int _frames = 0;
    private float _timeLeft;

    private void Awake() => _timeLeft = updateInterval;

    private void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;

        _accum += 1f / Mathf.Max(deltaTime, 0.0001f);
        _frames++;
        _timeLeft -= deltaTime;

        if (_timeLeft <= 0f)
        {
            float fps = _accum / _frames;

            if (fps >= mediumFPSThreshold)
                fpsText.color = highFPSColor;
            else if (fps >= lowFPSThreshold)
                fpsText.color = mediumFPSColor;
            else
                fpsText.color = lowFPSColor;

            fpsText.SetText("FPS: {0:0}", fps);

            _timeLeft = updateInterval;
            _accum = 0f;
            _frames = 0;
        }
    }
}
