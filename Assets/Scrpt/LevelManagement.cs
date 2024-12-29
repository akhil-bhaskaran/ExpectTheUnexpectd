using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    private GameObject canvasPrefab;  // Prefab of the Canvas
    private GameObject canvasInstance; // Instance of the Canvas
    public GameObject pausePanel;
    public GameObject quizPanel;
    public GameObject gameOverPanel;
    public GameObject spike;

    public Rigidbody2D rb;
    Vector2 startpos;
    public int lives;// Reference to Pause Panel

    void Start()
    {
        
        // Check if canvasPrefab is assigned
       /* if (canvasPrefab != null)
        {
            Debug.Log("CanvasPrefab is assigned for this level.");

            // Instantiate the Canvas prefab
            canvasInstance = Instantiate(canvasPrefab, transform);
            Debug.Log("Canvas Prefab instantiated successfully.");

            // Find the PausePanel in the Canvas hierarchy
            Transform panelTransform = canvasInstance.transform.Find("OptionsLabel");
            if (panelTransform != null)
            {
                pausePanel = panelTransform.gameObject;
                pausePanel.SetActive(false); // Ensure it starts inactive
                Debug.Log("Pause Panel found and set to inactive.");
            }
            else
            {
                Debug.LogError("PausePanel not found in the Canvas Prefab! Check prefab structure.");
            }
        }
        else
        {
            Debug.LogError("Canvas Prefab is not assigned in the Inspector for this level!");
        }*/
        pausePanel.SetActive(false);
        Debug.Log("Pause Panel initiated.");
        quizPanel.SetActive(false);
        lives = 4;
        startpos = rb.transform.position;
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
        if(lives > 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            /* rb.transform.position = startpos;
             pausePanel.SetActive(false);
             gameOverPanel.SetActive(false);
             Time.timeScale = 1.0f;
             lives--;
             ResetTraps();*/
            lives--;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            Time.timeScale = 1.0f;  
        }
        else
        {
            quizPanel.SetActive(true);
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

    void Update()
    {
    }
}
