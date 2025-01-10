using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNext : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DataManager.Instance.LivesRemaining = 4;
            SceneManager.LoadScene(levelName);

        }
    }
}
