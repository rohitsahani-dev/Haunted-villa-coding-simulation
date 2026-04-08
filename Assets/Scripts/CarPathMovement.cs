using UnityEngine;

public class CarPathMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Smooth rotation
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentWaypoint++;

            // Loop path
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}