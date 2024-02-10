using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve timeCurve;

    private float timePoints;

    private bool canSlowTime;

    public delegate void TimeController(bool activate);
    public static TimeController timeController;
    void TimeControl(bool activate)
    {
        if (activate)
            StartCoroutine(SlowTime(1));
        else 
            StartCoroutine(NormalTime(1));
    }
    IEnumerator SlowTime(float duration)
    {
        while (duration >= 0)
        {
            duration -= Time.unscaledDeltaTime;
            Time.timeScale = timeCurve.Evaluate(duration);
            yield return null;
        }
        Time.timeScale = 0.3f;
    }

    IEnumerator NormalTime(float duration)
    {
        while (duration <= 1)
        {
            duration += Time.unscaledDeltaTime;
            Time.timeScale = timeCurve.Evaluate(duration);
            yield return null;
        }
    }
    private void OnEnable()
    {
        timeController += TimeControl;
    }
    private void OnDisable()
    {
        timeController -= TimeControl;
    }
}
