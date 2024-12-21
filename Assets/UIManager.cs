using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public void loadLevel(int level) { 
        SceneManager.LoadScene("Level"+level);
    }
    public void loadHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

}
