using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject gameoverpanel,quizpanel,pausepanel;
    public GameObject canvas;
    public Rigidbody2D rb;
    Vector2 startpos;
    public GameObject trap;
    public  int lives;

    void Start()
    {
          startpos = rb.transform.position;
          lives = 4;
          quizpanel.SetActive(false);
    }
    void Update()
    {
        
    }
    public void Restart()
    {
        if ( lives > 0)
        {
            Time.timeScale = 1f;
            rb.transform.position = startpos;
            ResetTraps();
            gameoverpanel.SetActive(false);
            lives--;
        }
        else
        {
            Time.timeScale = 1f;
            gameoverpanel.SetActive(false);
            quizpanel.SetActive(true);
        }

       
    }
    public void Exit()
    {
        SceneManager.LoadScene("HomePage");

    }
    void ResetTraps()
    {
        trap.SetActive(false);
    }
    public void Pause()
    {
        pausepanel.SetActive(true);
        Time.timeScale = 0f;
    }
   public void  Resume()
    {
        pausepanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
