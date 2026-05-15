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

    private Quaternion openRotation;

    // Door sound
    private AudioSource audioSource;

    void Start()
    {
        // Get Audio Source
        audioSource = GetComponent<AudioSource>();

        // Hide UI at start
        interactText.SetActive(false);
        questionPanel.SetActive(false);

        // Clear texts
        messageText.text = "";
        answerInput.text = "";

        // Door open rotation
        openRotation = Quaternion.Euler(
            door.eulerAngles.x,
            door.eulerAngles.y + openAngle,
            door.eulerAngles.z
        );

        // Set question text
        questionText.text = question;
    }

    void Update()
    {
        // Open quiz panel with E
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

        // Hide interaction text
        interactText.SetActive(false);

        // Show question panel
        questionPanel.SetActive(true);

        // Clear old text
        answerInput.text = "";
        messageText.text = "";

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Focus input field
        answerInput.ActivateInputField();
    }

    public void CheckAnswer()
    {
        string answer = answerInput.text.Trim().ToLower();

        // Correct Answer
        if (answer == correctAnswer.ToLower())
        {
            messageText.text = "Correct! Door Opened.";

            // Open door
            doorOpened = true;

            // Play door sound
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Close panel after 2 seconds
            Invoke(nameof(CloseQuizPanel), 2f);
        }
        else
        {
            messageText.text = "Wrong Answer. Try Again.";

            // Clear input field
            answerInput.text = "";

            // Focus input again
            answerInput.ActivateInputField();

            // Remove wrong message after delay
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
        // Hide panel
        questionPanel.SetActive(false);

        // Clear UI
        messageText.text = "";
        answerInput.text = "";

        quizOpened = false;

        // Lock cursor again
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