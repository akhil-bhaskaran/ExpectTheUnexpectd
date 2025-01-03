using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePageController : MonoBehaviour
{

   public GameObject LevelSelection ;
    public GameObject HomePanel ;
    public TextMeshProUGUI usernameHolder;
   public static FirebaseAuth auth ;
    
    private void Awake()
    {
        Debug.Log(DataManager.Instance.Username);
        //initialise the dbrefs
        auth = FirebaseAuth.DefaultInstance;
        usernameHolder.text=DataManager.Instance.Username;
    }
    public void GotoLevelSelection()
    {
        HomePanel.SetActive(false);
        LevelSelection.SetActive(true); 
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
