using System;
using TMPro;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    [SerializeField] private float timeSpeedMultiplier = 30f;

    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform hourHand;

    private float timeElapsed = 0f;
    private bool timerActive = true;

    private readonly int startHour = 7;
    private readonly int endHour = 28;

    void Update()
    {
        UpdateTimeUI();
        UpdateClockHands();
    }

    private void UpdateTimeUI()
    {
        if (!timerActive) return;

        timeElapsed += Time.deltaTime * timeSpeedMultiplier;

        TimeSpan currentTime = TimeSpan.FromSeconds(timeElapsed) + TimeSpan.FromHours(startHour);

        if (currentTime.TotalHours >= endHour)
        {
            timerActive = false;
            currentTime = TimeSpan.FromHours(endHour);
        }

        string formattedTime = string.Format("{0:D2}:{1:D2}", (int)currentTime.TotalHours % 24, currentTime.Minutes);

        if (_timeText != null)
        {
            _timeText.text = formattedTime;
        }
    }

    private void UpdateClockHands()
    {
        if (!timerActive) return;

        TimeSpan currentTime = TimeSpan.FromSeconds(timeElapsed) + TimeSpan.FromHours(startHour);

        float minutes = (float)currentTime.TotalMinutes % 60f;
        float hours = (float)currentTime.TotalHours % 12f;

        float minuteAngle = Mathf.Lerp(5.61f, 365.61f, minutes / 60f);
        if (minuteHand != null)
        {
            minuteHand.localEulerAngles = new Vector3(minuteHand.localEulerAngles.x, minuteHand.localEulerAngles.y, minuteAngle);
        }

        float hourAngle = 180f + (hours * 30f);
        if (hourHand != null)
        {
            hourHand.localEulerAngles = new Vector3(hourHand.localEulerAngles.x, hourHand.localEulerAngles.y, hourAngle);
        }
    }
}
