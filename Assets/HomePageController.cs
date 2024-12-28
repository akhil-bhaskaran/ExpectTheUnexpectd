using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageController : MonoBehaviour
{

   public GameObject LevelSelection ;
    public GameObject HomePanel ;
    private void Awake()
    {
        //initialise the dbrefs
    }
    public void GotoLevelSelection()
    {
        HomePanel.SetActive(false);
        LevelSelection.SetActive(true); 
    }

}
