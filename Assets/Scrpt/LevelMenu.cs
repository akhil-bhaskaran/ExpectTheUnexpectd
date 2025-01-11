using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    private void Awake()
    {
        int unlockedLevels=DataManager.Instance.UnlockedLevel;
        for(int i =0; i<buttons.Length; i++)
        {
            buttons[i].interactable= false;
        }
        for (int i = 0; i < unlockedLevels; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void openLevel(int levelNumber)
    {
        string levelname= "Level "+levelNumber;
        SceneManager.LoadScene(levelname);
    }
    
}
