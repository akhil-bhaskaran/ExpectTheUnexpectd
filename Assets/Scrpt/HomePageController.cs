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
     DateTime? endTime;
    TimeSpan difference;
    string formatTime;
    private void Awake()
    {
        Debug.Log(DataManager.Instance.Username);
        Debug.Log(DataManager.Instance.endtime);
        //initialise the dbrefs
        auth = FirebaseAuth.DefaultInstance;
        usernameHolder.text=DataManager.Instance.Username;

        string endTimeString = DataManager.Instance.endtime;

        if (endTimeString != null && endTimeString != "")
        {

            endTime = DateTime.Parse(DataManager.Instance.endtime);
        }
        else {
            endTime = null;
        }

    }
   
    private void Update()
    {
        if (endTime != null && endTime > DateTime.UtcNow)
        {
            difference = (TimeSpan)(endTime - DateTime.UtcNow);
            formatTime = $"{difference.Minutes:D2}:{difference.Seconds:D2}";
            timer.text = formatTime;
        }
        else { 
            timer.text = "00:00";

            DataManager.Instance.TimeBreak=false;
            if (DataManager.Instance.LivesRemaining<=0)
            {
                DataManager.Instance.LivesRemaining=4;
                StartCoroutine(DataManager.Instance.SaveDataToFirebase());
            }
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
            Debug.Log("Cant play");
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
