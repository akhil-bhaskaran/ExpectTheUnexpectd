using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePageController : MonoBehaviour
{
    public TextMeshProUGUI timer;
   public GameObject LevelSelection ;
    public GameObject HomePanel ;
    public TextMeshProUGUI usernameHolder;
   public static FirebaseAuth auth ;
     DateTime endTime;
    TimeSpan difference;
    string formatTime;
    private void Awake()
    {
        Debug.Log(DataManager.Instance.Username);
        //initialise the dbrefs
        auth = FirebaseAuth.DefaultInstance;
        usernameHolder.text=DataManager.Instance.Username;


    }
    private void Start()
    {
        string endTimeString = DataManager.Instance.endtime;
        if (endTimeString != null)
        {
            endTime = DateTime.Parse(DataManager.Instance.endtime);
        }
    }
    private void Update()
    {
        if (endTime != null && endTime > DateTime.UtcNow)
        {
            difference = endTime - DateTime.UtcNow;
            formatTime = $"{difference.Minutes:D2}:{difference.Seconds:D2}";
            timer.text = formatTime;
        }
        else { 
            timer.text = "00:00";
            DataManager.Instance.TimeBreak=false;
        }
        
    }
    public void GotoLevelSelection()
    {
        HomePanel.SetActive(false);
        LevelSelection.SetActive(true); 
    }
    public void ShowLevelPanel() {
        if (!DataManager.Instance.TimeBreak)
        {
            LevelSelection.SetActive(true);
        }
        else {
            Debug.Log("CAnt play");
        }
    }
    public void Logout() {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
            Debug.Log("User logged out");
            SceneManager.LoadScene("AuthPage");
        }
        else {
            Debug.Log("user doesnt exist");
        }
    }

}
