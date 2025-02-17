using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeAudio : MonoBehaviour
{
    public AudioSource audioSource; 

    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play(); 
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
