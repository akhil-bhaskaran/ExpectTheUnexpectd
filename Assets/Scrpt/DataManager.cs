using UnityEngine;

public class DataManager : MonoBehaviour
{
    string UserD;
 
    public static DataManager Instance { get; private set; }

    // Fields to store game-related data
    public string UserId{ get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int CurrentLevel { get; set; }
    public int UnlockedLevel { get; set; }
    public int LivesRemaining { get; set; }
    public bool TimeBreak { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Optional: Method to initialize default values
    public void InitializeGameData(string usrid,string username, string email)
    {
        UserId = usrid; 
        Username = username;
        Email = email;
        CurrentLevel = 1;         
        UnlockedLevel = 1;        
        LivesRemaining = 5;       
        TimeBreak = false;       
    }

 
    

  
   
}

