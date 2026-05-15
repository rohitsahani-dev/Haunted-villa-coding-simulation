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

    // NEW
    private bool gameWon = false;

    void Start()
    {
        gameOverPanel.SetActive(false);

        UpdateTimerDisplay();
    }

    void Update()
    {
        // Stop everything if game won
        if (gameWon)
            return;

        // Stop if timer paused
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

        RestartGame();
    }

    async void RestartGame()
    {
        // Wait 3 seconds in real time
        await System.Threading.Tasks.Task.Delay(3000);

        // Reset time scale
        Time.timeScale = 1f;

        // Reload scene
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // CALLED WHEN PLAYER WINS
    public void StopTimer()
    {
        gameWon = true;

        timerRunning = false;
    }
}