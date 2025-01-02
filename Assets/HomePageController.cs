using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePageController : MonoBehaviour
{

   public GameObject LevelSelection ;
    public GameObject HomePanel ;
   public static FirebaseAuth auth ;
    private void Awake()
    {
        //initialise the dbrefs
        auth = FirebaseAuth.DefaultInstance;
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
