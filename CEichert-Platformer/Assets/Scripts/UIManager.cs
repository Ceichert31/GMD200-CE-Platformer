using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI
        timeText,
        healthText;

    public delegate void SetTime(float time);
    public static SetTime setTimeText;

    public delegate void UpdateHealth(int health);
    public static UpdateHealth updateHealth;

    /// <summary>
    /// Set the current time value to UI
    /// </summary>
    /// <param name="time"></param>
    void SetTimeText(float time)
    {
        timeText.text = Mathf.FloorToInt(time).ToString();
    }

    void UpdateHealthUI(int health)
    {
        healthText.text = health.ToString();
    }

    private void OnEnable()
    {
        setTimeText += SetTimeText;
        updateHealth += UpdateHealthUI;
    }
    private void OnDisable()
    {
        setTimeText -= SetTimeText;
        updateHealth -= UpdateHealthUI;
    }
}
