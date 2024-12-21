using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using UnityEngine.SceneManagement;
using System.Threading;
public class SplashScreen : MonoBehaviour
{
    //fireabse variables
    FirebaseAuth auth;


    private SynchronizationContext unityContext;

    //Common script variables
    public float splashScreenDuration;


    private void Awake()
    {
        unityContext = SynchronizationContext.Current;
        StartCoroutine(SplashSequence());
    }
    public IEnumerator SplashSequence()
    {
        yield return new WaitForSeconds(splashScreenDuration);
        initializeFirebase();
    }
    private void initializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task=>{
            if (task.Result == DependencyStatus.Available)
            {
                // Ensure this runs on the main thread
                RunOnMainThread(() =>
                {
                    
                    auth = FirebaseAuth.DefaultInstance;
                    Debug.Log("Firebase initialized successfully.");
                    CheckAuthentication();
                });
            }
            else
            {
                Debug.LogError($"Firebase initialization error: {task.Result}");

                // Load the fallback scene on the main thread
                RunOnMainThread(() => LoadScene("AuthPage"));
            }
        });
    }
    private void CheckAuthentication()
    {
        RunOnMainThread(async () =>
        {
            FirebaseUser user = auth?.CurrentUser;


            Debug.Log($"Current User: {(user != null ? "Exists" : "Null")}");

            if (user != null)
            {
                await user.ReloadAsync();
                if (user.IsEmailVerified)
                {
                    Debug.Log("Authenticated and email verified. Loading HomeScene.");
                   
                    LoadScene("HomePage");
                }
                else
                {
                    Debug.Log("Authenticated but email not verified. Loading FirebaseAuth.");
                    LoadScene("AuthPage");
                }
            }
            else
            {
                Debug.Log("No authenticated user. Loading FirebaseAuth.");
                LoadScene("AuthPage");
            }
        });
    }

  


    private void RunOnMainThread(System.Action action)
    {
        // Post the action to Unity's main thread
        unityContext.Post(_ => action(), null);
    }
    private void LoadScene(string sceneName)
    {
        Debug.Log($"Attempting to load scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
