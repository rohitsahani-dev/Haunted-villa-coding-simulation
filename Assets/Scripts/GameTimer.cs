using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Header("Timer UI")]
    public TMP_Text timerText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;

    [Header("Timer Settings")]
    public float timeRemaining = 120f;

    private bool timerRunning = true;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;

            GameOver();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text =
            string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        InvokeRealtimeRestart();
    }

    async void InvokeRealtimeRestart()
    {
        await System.Threading.Tasks.Task.Delay(3000);

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}