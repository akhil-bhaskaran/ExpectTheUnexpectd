using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject targetObject; // The object to be moved
    public Transform[] positions;  // Assign four empty GameObjects as waypoints
    public float moveSpeed = 10f; // Adjust for speed control

    private int currentTargetIndex = 0;
    private bool isMoving = false;
    private bool hasMoved = false;

    void Update()
    {
        if (!isMoving || positions.Length == 0 || targetObject == null) return; // Move only if triggered and target is assigned

        // Move towards the current target position
        targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, positions[currentTargetIndex].position, moveSpeed * Time.deltaTime*2);

        // Check if we reached the target
        if (Vector3.Distance(targetObject.transform.position, positions[currentTargetIndex].position) < 0.1f)
        {
            // Move to the next target
            currentTargetIndex++;
            
            if (currentTargetIndex >= positions.Length)
            {
                isMoving = false; // Stop moving after reaching the last position
                hasMoved = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasMoved) // Ensure the player has the correct tag and only trigger once
        {
            isMoving = true;
        }
    }
}