using UnityEngine;
using UnityEngine.UI;

public class DoorPuzzle : MonoBehaviour
{
    public GameObject questionPanel;

    public InputField answerInput;

    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;

    private bool playerNear = false;

    void Update()
    {
        if(playerNear && Input.GetKeyDown(KeyCode.E))
        {
            questionPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CheckAnswer()
    {
        if(answerInput.text == "0 1 2 3 4")
        {
            leftDoorAnimator.SetTrigger("Open");
            rightDoorAnimator.SetTrigger("Open");

            questionPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
