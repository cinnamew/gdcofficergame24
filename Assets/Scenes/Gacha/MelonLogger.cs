using DG.Tweening;
using TMPro;
using UnityEngine;

public enum LogType
{
    Message,
    Success,
    Warning,
    Error
}

/// <summary>
/// Custom logger for the app which just displays visual messages to the screen.
/// </summary>
public class MelonLogger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _logObject;

    private static TextMeshProUGUI _log;
    private static Transform _canvas;
    
    private static MelonLogger _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _log = _logObject;
        _canvas = Instantiate(Resources.Load<Canvas>("LogCanvas").gameObject).transform;
    }

    private static Color LogTypeToColor(LogType logType) =>
        logType switch
        {
            LogType.Success => new Color(0.0f, 0.6f, 0.0f),
            LogType.Warning => Color.yellow,
            LogType.Error => Color.red,
            _ => Color.white
        };

    /// <summary>
    /// Displays a message to the screen for a short time.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="logType">The log type of the message.</param>
    /// <param name="duration">The duration of the message.</param>
    public static void Log(string message, LogType logType = LogType.Message, float duration = 3f)
    {
        var tempLog = Instantiate(_log, _canvas);
        tempLog.text = message;
        tempLog.color = LogTypeToColor(logType);
        
        tempLog.DOFade(0f, 1f).SetDelay(duration - 1.25f);
        tempLog.rectTransform.DOAnchorPosY(-300f, duration).SetEase(Ease.OutCubic).OnComplete(() => Destroy(tempLog.gameObject));
    }
    
    /// <summary>
    /// Displays a message to the screen for a short time.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="predicate">The predicate to check.</param>
    /// <param name="logType">The log type of the message.</param>
    public static void LogUntil(string message, System.Func<bool> predicate, LogType logType = LogType.Message)
    {
        var tempLog = Instantiate(_log, _canvas);
        tempLog.text = message;
        tempLog.color = LogTypeToColor(logType);
        
        //_instance.DoAfter(predicate, () => Destroy(tempLog.gameObject));
    }
}