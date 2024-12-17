using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject gameOver;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dead();
        }
    }

    void Dead()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
