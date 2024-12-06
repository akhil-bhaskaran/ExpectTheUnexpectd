using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel;
    void Start()
    {
        
        
    }
    public void gotoOne()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void gotoTwo()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void gotoMainMenu() {
        SceneManager.LoadScene("HomePage");
        Time.timeScale = 1.0f;
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }
    public void stopGame() { 

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void resume()
    {

        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
