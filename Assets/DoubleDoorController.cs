using UnityEngine;

public class DoubleDoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openAngle = 90f;
    public float speed = 2f;

    private bool isOpen = false;

    private Quaternion leftClosedRot;
    private Quaternion rightClosedRot;

    private Quaternion leftOpenRot;
    private Quaternion rightOpenRot;

    void Start()
    {
        leftClosedRot = leftDoor.rotation;
        rightClosedRot = rightDoor.rotation;

        leftOpenRot = Quaternion.Euler(
            leftDoor.eulerAngles.x,
            leftDoor.eulerAngles.y - openAngle,
            leftDoor.eulerAngles.z
        );

        rightOpenRot = Quaternion.Euler(
            rightDoor.eulerAngles.x,
            rightDoor.eulerAngles.y + openAngle,
            rightDoor.eulerAngles.z
        );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            leftDoor.rotation = Quaternion.Slerp(
                leftDoor.rotation,
                leftOpenRot,
                Time.deltaTime * speed
            );

            rightDoor.rotation = Quaternion.Slerp(
                rightDoor.rotation,
                rightOpenRot,
                Time.deltaTime * speed
            );
        }
        else
        {
            leftDoor.rotation = Quaternion.Slerp(
                leftDoor.rotation,
                leftClosedRot,
                Time.deltaTime * speed
            );

            rightDoor.rotation = Quaternion.Slerp(
                rightDoor.rotation,
                rightClosedRot,
                Time.deltaTime * speed
            );
        }
    }
}
