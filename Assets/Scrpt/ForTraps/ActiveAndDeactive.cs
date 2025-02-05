using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAndDeactive : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public float initialDelay = 0.5f; // Starting delay
    public float minDelay = 0.05f; // Minimum delay to prevent too fast execution
    public float speedIncreaseFactor = 0.9f; // Factor to reduce delay
    private bool isActivated = false;
    private int tries = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isActivated)
        {
            StartCoroutine(ActivateAndDeactivateSequence(initialDelay));
            if (tries <= 0)
                isActivated = true;
            tries--;
            Debug.Log(tries + " Time of loading");
        }
    }

    IEnumerator ActivateAndDeactivateSequence(float delay)
    {
        while (delay > minDelay) // Loop until delay reaches minDelay
        {
            // Activate objects one by one with decreasing delay
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
                yield return new WaitForSeconds(delay);
            }

            // Deactivate objects one by one with the same decreasing delay
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(false);
                yield return new WaitForSeconds(delay);
            }

            delay *= speedIncreaseFactor; // Reduce delay to speed up activation
        }
    }
}
