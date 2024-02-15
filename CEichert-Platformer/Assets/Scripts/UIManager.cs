using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    public delegate void SetTime(float time);
    public static SetTime setTimeText;

    /// <summary>
    /// Set the current time value to UI
    /// </summary>
    /// <param name="time"></param>
    void SetTimeText(float time)
    {
        timeText.text = Mathf.FloorToInt(time).ToString();
    }

    private void OnEnable()
    {
        setTimeText += SetTimeText;
    }
    private void OnDisable()
    {
        setTimeText -= SetTimeText;
    }
}
