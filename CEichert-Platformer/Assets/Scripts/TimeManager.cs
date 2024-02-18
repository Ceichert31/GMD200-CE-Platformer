using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve timeCurve;

    [SerializeField] private float timePoints = 10;

    private bool canSlowTime = true;

    public delegate void TimeController(bool activate);
    public static TimeController timeController;

    private void Update()
    {
        //If time is normal, add to timepoints
        if (Time.timeScale == 1)
        {
            if (canSlowTime)
                timePoints += Time.unscaledDeltaTime;
        }
        //If time is slowed, subtract from timepoints, and exit slowed time if it hits zero
        else if (Time.timeScale < 1)
            {
                timePoints -= Time.unscaledDeltaTime;
                if (timePoints <= 0)
                {
                    StartCoroutine(NormalTime(1));
                    canSlowTime = false;
                    Invoke(nameof(TimeRegen), 3);
                }
            }
        timePoints = Mathf.Clamp(timePoints, 0, 10);

        UIManager.setTimeText?.Invoke(timePoints);

    }
    void TimeControl(bool activate)
    {
        if (!canSlowTime)
            return;

        if (activate)
            StartCoroutine(SlowTime(1));
        else
            StartCoroutine(NormalTime(0));
    }
    IEnumerator SlowTime(float duration)
    {
        GlobalVolumeController.timeEffects?.Invoke();
        CameraFollow.zoom?.Invoke(true);
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
        GlobalVolumeController.timeEffects?.Invoke();
        CameraFollow.zoom?.Invoke(false);
        while (duration <= 1)
        {
            duration += Time.unscaledDeltaTime;
            Time.timeScale = timeCurve.Evaluate(duration);
            yield return null;
        }
    }
    void TimeRegen()
    {
        canSlowTime = true;
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
