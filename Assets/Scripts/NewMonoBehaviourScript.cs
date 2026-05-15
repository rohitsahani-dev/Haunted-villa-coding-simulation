using UnityEngine;
using TMPro;

public class FinalGameManager : MonoBehaviour
{
    [Header("Winning UI")]
    public GameObject winPanel;
    public TMP_Text winText;

    [Header("Final Exit Door")]
    public Transform finalDoor;

    public float openAngle = -120f;
    public float openSpeed = 2f;

    [Header("Player")]
    public GameObject player;

    private bool openDoor = false;

    private Quaternion targetRotation;

    void Start()
    {
        winPanel.SetActive(false);

        targetRotation = Quaternion.Euler(
            finalDoor.eulerAngles.x,
            finalDoor.eulerAngles.y + openAngle,
            finalDoor.eulerAngles.z
        );
    }

    void Update()
    {
        // Open final exit door smoothly
        if (openDoor)
        {
            finalDoor.rotation = Quaternion.Lerp(
                finalDoor.rotation,
                targetRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    public void WinGame()
    {
        StartCoroutine(WinSequence());
    }

    System.Collections.IEnumerator WinSequence()
    {
        // Freeze player temporarily
        player.SetActive(false);

        // Show win UI
        winPanel.SetActive(true);

        winText.text = "YOU ESCAPED";

        // Wait 3 seconds
        yield return new WaitForSeconds(3f);

        // Hide win UI
        winPanel.SetActive(false);

        // Open final exit
        openDoor = true;

        // Give control back
        player.SetActive(true);
    }
}