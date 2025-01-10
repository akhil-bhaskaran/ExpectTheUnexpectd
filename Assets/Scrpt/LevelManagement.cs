using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    private GameObject canvasPrefab;  
    private GameObject canvasInstance; 
    public GameObject pausePanel;
    public GameObject quizPanel;
    public GameObject gameOverPanel;
    public GameObject spike;

   

    //Firebase Declarations
    public static DatabaseReference dbref;
    void Start()
    {
        dbref = FirebaseDatabase.DefaultInstance.RootReference;
        pausePanel.SetActive(false);
        Debug.Log("Pause Panel initiated.");
        quizPanel.SetActive(false);
       
    }

    

    public void gotoOne()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void gotoTwo()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene("HomePage");
        Time.timeScale = 1.0f;
    }

    public void restartGame()
    {
        if(DataManager.Instance.LivesRemaining > 0)
        {
            
            DataManager.Instance.LivesRemaining--;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            Time.timeScale = 1.0f;  
        }
        else
        {
            quizPanel.SetActive(true);
            DataManager.Instance.LivesRemaining = 4;
        }
       
    }
    void ResetTraps()
    {
        spike.SetActive(false);
    }
    public void stopGame()
    {
        Debug.Log("stop game called");
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Game paused. PausePanel activated.");
        }
        else
        {
            Debug.LogError("PausePanel is not assigned or instantiated!");
        }
    }

    public void resume()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1.0f;
            Debug.Log("Game resumed. PausePanel deactivated.");
        }
        else
        {
            Debug.LogError("PausePanel is not assigned or !");
        }
    }

    private void UpdateValue(string path, object newValue)
    {
        dbref.Child(path).SetValueAsync(newValue).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log($"Value at {path} updated successfully to: {newValue}");
            }
            else
            {
                Debug.LogError($"Failed to update value at {path}: {task.Exception}");
            }
        });
    }
}
