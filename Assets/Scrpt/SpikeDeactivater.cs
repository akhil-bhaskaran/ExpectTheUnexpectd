using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDeactivater : MonoBehaviour
{
    public List<GameObject> spikes; 
    public float delay = 1f; 
    bool isActived=false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            StartCoroutine(DeactivateSpikes());
            isActived=true;
        }
    }
    IEnumerator DeactivateSpikes()
    {
        foreach (GameObject spike in spikes)
        {
            if (spike != null)
            {
                spike.SetActive(false);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
