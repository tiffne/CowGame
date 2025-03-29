using UnityEngine;
using TMPro;

public class WorkDayTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay;
    [SerializeField] private float dayDurationInMinutes;

    private float currentTime; // 0 = 9 AM, 1 = 9 PM
    private bool isDayActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<WorkDayTimer>().StartDay();
        }

        if (!isDayActive) return;

        currentTime += Time.deltaTime / (dayDurationInMinutes * 60);
        
        UpdateTimeDisplay();

        if(currentTime >= 1f)
        {
            EndDay();
        }
    }

    private void UpdateTimeDisplay()
    {
        float hoursPassed = currentTime * 12f;
        int currentHour = 9 + Mathf.FloorToInt(hoursPassed);
        int currentMinute = Mathf.FloorToInt((hoursPassed % 1) * 60);

        string amPm = currentHour >= 12 ? "PM" : "AM";
        int displayHour = currentHour > 12 ? currentHour - 12 : currentHour;

        timeDisplay.text = $"{displayHour:00}:{currentMinute:00} {amPm}";
    }

    public void StartDay()
    {
        currentTime = 0f;
        isDayActive = true;
        Debug.Log("Day started!");
    }

    public void EndDay()
    {
        isDayActive = false;
        Debug.Log("Day ended!");
        
    }

    public float GetNormalizedTime() // Returns current progress of day as a value between 0 (9 AM) and 1 (9 PM).
    {
        return currentTime;
    }
}