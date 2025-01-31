using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boom : MonoBehaviour
{
    public GameObject gameOver;
    
    private void Awake()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dead();
        }
    }

    void Dead()
    {
        restartGame();
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restartGame()
    {
        if (DataManager.Instance.LivesRemaining > 0)
        {
            int livesrem = PlayerPrefs.GetInt("LivesRemaining");
            DataManager.Instance.LivesRemaining--;
            PlayerPrefs.SetInt("LivesRemaining", (livesrem - 1));
            PlayerPrefs.Save();
        }
       

    }
}
