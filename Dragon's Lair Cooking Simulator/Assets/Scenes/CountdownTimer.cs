using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add this for TextMeshPro

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 120f; // 2 minutes
    public TextMeshProUGUI timerText; // Change to TextMeshProUGUI
    private bool isTimerRunning = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (timeRemaining <= 0)
        {
            isTimerRunning = false;
            timeRemaining = 0;
            UpdateTimerDisplay();
            Debug.Log("Timer ended!");
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}