using UnityEngine;

public class InvisibleTrigger : MonoBehaviour
{
    public GameObject obstaclesToTrigger; // Obstacles to trigger upon interaction

    private void Start()
    {
      /*  foreach (GameObject obstacle in obstaclesToTrigger)
        {
            // Activate each obstacle or trigger its actions
            obstacle.SetActive(false);
            // Example: You can also add additional actions here like triggering animations, sounds, etc.
        }*/obstaclesToTrigger.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger obstacles/actions
            TriggerObstacles();
        }
    }

    private void TriggerObstacles()
    {
        
            obstaclesToTrigger.SetActive(true);
      /*  foreach (GameObject obstacle in obstaclesToTrigger)
        {
            // Activate each obstacle or trigger its actions
            obstacle.SetActive(true);
            // Example: You can also add additional actions here like triggering animations, sounds, etc.
        }*/
    }
}
