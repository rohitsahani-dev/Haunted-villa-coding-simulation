using UnityEngine;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class PythonPuzzle
{
    [TextArea(3, 5)]
    public string codeQuestion; 
    public int correctAnswer;   
}

public class DoorQuizController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject quizPanel;
    public TMP_Text questionText;

    [Header("Door Movement Settings")]
    public Transform doorTransform;
    public Vector3 targetOpenRotation = new Vector3(0, 90, 0); 
    public float openSpeed = 3f;

    [Header("Puzzles List")]
    public List<PythonPuzzle> puzzleList = new List<PythonPuzzle>();

    private bool isPlayerNearby = false;
    private bool isDoorOpening = false;
    private Quaternion targetQuaternion;
    private int currentCorrectAnswer;
    private string originalQuestionText;

    void Start()
    {
        if (quizPanel != null) quizPanel.SetActive(false);
        if (doorTransform != null)
        {
            targetQuaternion = Quaternion.Euler(doorTransform.localEulerAngles + targetOpenRotation);
        }
    }

    void Update()
    {
        bool interactPressed = false;

        // Detects mouse clicks across both old and new input systems safely
#if ENABLE_INPUT_SYSTEM
        if (UnityEngine.InputSystem.Mouse.current != null && UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame) interactPressed = true;
#else
        if (Input.GetMouseButtonDown(0)) interactPressed = true;
#endif

        if (isPlayerNearby && !isDoorOpening && interactPressed && !quizPanel.activeSelf)
        {
            OpenQuizUI();
        }

        if (isDoorOpening && doorTransform != null)
        {
            doorTransform.localRotation = Quaternion.Slerp(doorTransform.localRotation, targetQuaternion, Time.deltaTime * openSpeed);
            if (Quaternion.Angle(doorTransform.localRotation, targetQuaternion) < 0.1f)
            {
                doorTransform.localRotation = targetQuaternion;
                isDoorOpening = false;
            }
        }
    }

    void OpenQuizUI()
    {
        if (puzzleList.Count == 0) return;

        int randomIndex = Random.Range(0, puzzleList.Count);
        PythonPuzzle selectedPuzzle = puzzleList[randomIndex];

        originalQuestionText = selectedPuzzle.codeQuestion;
        questionText.text = originalQuestionText;
        currentCorrectAnswer = selectedPuzzle.correctAnswer;

        quizPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SubmitAnswerNumber(int chosenNumber)
    {
        if (chosenNumber == currentCorrectAnswer)
        {
            isDoorOpening = true;
            quizPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            questionText.text = "<color=red>Wrong! Try again:</color>\n\n" + originalQuestionText;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (quizPanel != null) quizPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
