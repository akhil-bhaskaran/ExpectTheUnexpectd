using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatfrom : MonoBehaviour
{
    bool isActive = false;
    public GameObject platform;

    public Vector3 startPosition; 
    public Vector3 endPosition;   
    public float speed = 2f;        

    private float t = 0f;

    void Start()
    {
        startPosition = platform.transform.position;
        platform.transform.position = startPosition;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isActive = true;
            Debug.Log("Enteredd");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (t < 1f)
            {
                t += Time.deltaTime * speed;
                platform.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            }
        }
    }
}
