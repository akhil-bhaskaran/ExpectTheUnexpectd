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
    public static DatabaseReference dbref ;

    //game objects
    public GameObject StatusPanel;

     DateTime? endTime;
    TimeSpan difference;
    string formatTime;
    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbref = FirebaseDatabase.DefaultInstance.RootReference;
        int isNewUser = PlayerPrefs.GetInt("IsNewUser", 0);
        if (isNewUser == 1 && DataManager.Instance.Username==null) {
           
            CreateNewUserData(auth.CurrentUser.UserId,auth.CurrentUser.Email);
        }
        Debug.Log(DataManager.Instance.Username);
        Debug.Log(DataManager.Instance.endtime);
        //initialise the dbrefs
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
            ShowStatusPanel("Cant Play Now !");
        }
    }
    public void Logout() {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
            Debug.Log("User logged out");
            ResetGameData();
            SceneManager.LoadScene("AuthPage");
        }
        else {
            Debug.Log("user doesnt exist");
        }
    }
    public void CreateNewUserData(string userId, string email)
    {

        DataToSave userdata = new DataToSave(email)
        {
            userId = PlayerPrefs.GetString("UserId"),
            Username = PlayerPrefs.GetString("Username"),
            Email = email,
            ReachedIndex = 3,
            endtime = null,
            UnlockedLevel = 1,
            LivesRemaining = 4,
            TimeBreak = false,
        };

        string json = JsonUtility.ToJson(userdata);
        dbref.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("User data saved successfully in Firebase.");
                PlayerPrefs.SetInt("isNewUser", 0);
                PlayerPrefs.Save();
                DataManager.Instance.InitializeGameData(userdata.userId, userdata.Username, userdata.Email);
            }
            else
            {
                Debug.LogError("Failed to save user data: " + task.Exception?.Message);
            }
        });

       
    }
    void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs cleared!");
    }
    public void ShowStatusPanel(string msg)
    {

        TextMeshProUGUI statusText = StatusPanel.GetComponentInChildren<TextMeshProUGUI>();
        statusText.text = msg;
        StatusPanel.SetActive(true); // Show the panel
        StartCoroutine(HideStatusPanelAfterDelay(4f)); // Call the coroutine to hide it after 4 seconds
    }

    // Coroutine to hide the status panel after a delay
    private IEnumerator HideStatusPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay (4 seconds)
        StatusPanel.SetActive(false);
    }

}
