using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform player;          // Player Transform
    public float openDistance = 3f;   // Distance to interact
    public float openAngle = 90f;     // Door rotation angle
    public float smooth = 2f;         // Smooth speed

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + openAngle,
            transform.eulerAngles.z
        );
    }

    void Update()
    {
        // Check player distance
        float distance = Vector3.Distance(player.position, transform.position);

        // Press E to open/close
        if (distance <= openDistance && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        // Smooth door rotation
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                openRotation,
                Time.deltaTime * smooth
            );
        }
        else
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                closedRotation,
                Time.deltaTime * smooth
            );
        }
    }
}