using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNext : MonoBehaviour
{
    int currentIndex;
    string levelName;
    private void Awake()
    {
         currentIndex= SceneManager.GetActiveScene().buildIndex;
        levelName = "Level " + (currentIndex - 1);

    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            DataManager.Instance.LivesRemaining = 4;
            UnlockNextLevel();
            SceneManager.LoadScene(levelName);

        }
    }
    void UnlockNextLevel()
    {
        if (currentIndex >= DataManager.Instance.ReachedIndex)
        {
            DataManager.Instance.ReachedIndex = currentIndex + 1;
            DataManager.Instance.UnlockedLevel++;
        }
    }
}
