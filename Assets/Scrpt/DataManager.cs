using Firebase.Auth;
using Firebase.Database;
using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour
{
    string UserD;
 
    public static DataManager Instance { get; private set; }

    // Fields to store game-related data
    public string UserId{ get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int ReachedIndex { get; set; }
    public string? endtime { get; set; }
    public int UnlockedLevel { get; set; }
    public int LivesRemaining { get; set; }
    public bool TimeBreak { get; set; }



    private FirebaseAuth auth;
    private DatabaseReference dbref;
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbref = FirebaseDatabase.DefaultInstance.RootReference;
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
        endtime=null;
        ReachedIndex = 3;
        UnlockedLevel = 1;        
        LivesRemaining = 4;       
        TimeBreak = false;       
    }
/*    private void OnApplicationPause(bool pause)
    {
        SaveDataToFirebase();
    }
    private void OnApplicationQuit()
    {
        SaveDataToFirebase();
    }*/

    public IEnumerator SaveDataToFirebase()
    {
        if (auth.CurrentUser == null)
        {
            Debug.LogError("No authenticated user. Cannot save data to Firebase.");
            yield break;
        }

        DataToSave dts = new DataToSave(Email)
        {
            userId = UserId,
            Username = Username,
            Email = Email,
            ReachedIndex = ReachedIndex,
            endtime = endtime,
            UnlockedLevel = UnlockedLevel,
            LivesRemaining = LivesRemaining,
            TimeBreak = TimeBreak,
        };

        var saveTask = dbref.Child("users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(JsonUtility.ToJson(dts));
        yield return new WaitUntil(() => saveTask.IsCompleted);
        if (saveTask.IsCompleted)
        {
            Debug.Log("Data saved to Firebase successfully.");
        }
        else
        {
            Debug.LogError("Failed to save data to Firebase: " + saveTask.Exception);
        }
        /*
        .ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data saved to Firebase successfully.");
                }
                else
                {
                    Debug.LogError("Failed to save data to Firebase: " + task.Exception);
                }
            });*/
    }





}

