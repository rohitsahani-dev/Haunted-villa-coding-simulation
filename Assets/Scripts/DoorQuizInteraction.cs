using UnityEngine;
using TMPro;

public class DoorQuizInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactText;
    public GameObject questionPanel;

    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public TMP_Text messageText;

    [Header("Door Settings")]
    public Transform door;
    public float openAngle = -120f;
    public float openSpeed = 2f;

    [Header("Question")]
    [TextArea]
    public string question =
        "What is the output of:\nprint(2 + 3)";

    public string correctAnswer = "5";

    private bool playerNear = false;
    private bool quizOpened = false;
    private bool doorOpened = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        interactText.SetActive(false);
        questionPanel.SetActive(false);

        messageText.text = "";

        closedRotation = door.rotation;

        openRotation = Quaternion.Euler(
            door.eulerAngles.x,
            door.eulerAngles.y + openAngle,
            door.eulerAngles.z
        );

        questionText.text = question;
    }

    void Update()
    {
        // Show question panel
        if (playerNear && !quizOpened && !doorOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenQuizPanel();
            }
        }

        // Smooth door opening
        if (doorOpened)
        {
            door.rotation = Quaternion.Lerp(
                door.rotation,
                openRotation,
                Time.deltaTime * openSpeed
            );
        }
    }

    void OpenQuizPanel()
    {
        quizOpened = true;

        interactText.SetActive(false);
        questionPanel.SetActive(true);

        answerInput.text = "";
        messageText.text = "";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        answerInput.ActivateInputField();
    }

    public void CheckAnswer()
    {
        string answer = answerInput.text.Trim();

        // CORRECT ANSWER
        if (answer == correctAnswer)
        {
            messageText.text = "Correct! Door Opened.";

            doorOpened = true;

            Invoke("CloseQuizPanel", 2f);
        }
        else
        {
            messageText.text = "Wrong Answer. Try Again.";

            answerInput.text = "";

            answerInput.ActivateInputField();

            CancelInvoke(nameof(ClearMessage));
            Invoke(nameof(ClearMessage), 2f);
        }
    }

    void ClearMessage()
    {
        if (!doorOpened)
        {
            messageText.text = "";
        }
    }

    void CloseQuizPanel()
    {
        questionPanel.SetActive(false);

        messageText.text = "";
        answerInput.text = "";

        quizOpened = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doorOpened)
        {
            playerNear = true;

            if (!quizOpened)
            {
                interactText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            interactText.SetActive(false);
        }
    }
}