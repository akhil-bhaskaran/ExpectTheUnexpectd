using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControll : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;
    [Range(0f, 1f)]
    public float smoothTime;
    public Vector3 possitionOffest;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        Vector3 targetPostion = target.position + possitionOffest;
        transform.position = Vector3.SmoothDamp(transform.position, targetPostion, ref velocity, smoothTime);
    }
}

