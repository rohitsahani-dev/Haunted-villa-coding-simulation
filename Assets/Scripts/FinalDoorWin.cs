using UnityEngine;
using TMPro;
using System.Collections;

public class FinalDoorWin : MonoBehaviour
{
    [Header("UI")]
    public GameObject winPanel;
    public TMP_Text winText;

    [Header("Final Door")]
    public Transform finalDoor;
    public float openAngle = -120f;
    public float openSpeed = 2f;

    private bool open;

    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        if (winPanel) winPanel.SetActive(false);

        closedRot = finalDoor.rotation;
        openRot = closedRot * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (open)
        {
            finalDoor.rotation = Quaternion.Lerp(
                finalDoor.rotation,
                openRot,
                Time.deltaTime * openSpeed
            );
        }
    }

   public void WinGame()
{
    // Stop timer
    FindFirstObjectByType<GameTimer>().StopTimer();

    StartCoroutine(WinSequence());
}

    IEnumerator WinSequence()
    {
        winPanel.SetActive(true);
        winText.text = "YOU ESCAPED";

        yield return new WaitForSeconds(3f);

        winPanel.SetActive(false);
        open = true;
    }
}