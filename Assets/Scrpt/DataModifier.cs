using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataModifier : MonoBehaviour
{
    public static DataModifier Instance { get; private set; }
  
    // Start is called before the first frame update
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);// Persist across scenes


        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("App is paused. Saving data to Firebase...");
            StartCoroutine(DataManager.Instance.SaveDataToFirebase());
        }
    }

    private void OnApplicationQuit()
    {
        //DataManager.Instance.SaveDataToFirebase();
        StartCoroutine(DataManager.Instance.SaveDataToFirebase());
        Debug.Log("Data is saving");
    }
}
