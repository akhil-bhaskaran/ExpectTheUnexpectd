using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeAudio : MonoBehaviour
{
    public AudioSource audioSource; 
    public GameObject sound;
    int isBgm;
    void Awake()
    {
        if(PlayerPrefs.HasKey("isMusic"))
        {
            isBgm=PlayerPrefs.GetInt("isMusic");
            Debug.Log("THis is your is music values::"+isBgm);
        }
        else
        {
            isBgm=1;
            PlayerPrefs.SetInt("isMusic",isBgm);
        }
    }
   void Start()
   {
    Debug.Log("Start: isBgm value = " + isBgm);
    
    if (isBgm == 1)
    {
        if (!audioSource.isPlaying)
        {
            Debug.Log("Playing music");
            audioSource.Play();
            sound.SetActive(true);
        }
    }
    else
    {
        Debug.Log("Stopping music");
        audioSource.Stop();
        sound.SetActive(false);
    }
    }

    public void StartMusic()
{
    PlayerPrefs.SetInt("isMusic", 1);
    PlayerPrefs.Save(); 
    isBgm = 1;  
    if (!audioSource.isPlaying)
    {
        audioSource.Play();
    }
}

public void StopMusic()
{
    Debug.Log("Stopping musci");
    PlayerPrefs.SetInt("isMusic", 0);
    PlayerPrefs.Save(); 
    isBgm = 0;
    audioSource.Stop();
}

}
