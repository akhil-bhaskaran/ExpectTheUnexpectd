using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class FirebaseManager : MonoBehaviour
{ 
    //Firebase declarations
    public static FirebaseAuth auth;
    public static DatabaseReference dbref;

    //Login GameObjects
    public TMP_InputField LoginEmail;
    public TMP_InputField Password;

    //SignUp GameObjects
    public TMP_InputField SignUpEmail;
    public TMP_InputField SignUpPassword;
    public TMP_InputField ConfirmPassword;
    public TMP_InputField Username;

    public GameObject StatusPanel;

    public bool isEmailver=false;
    

    private void Awake()
    {
        auth=FirebaseAuth.DefaultInstance;
        dbref= FirebaseDatabase.DefaultInstance.RootReference;

    }
    public void RegisterPlayer()
    {
        string email = SignUpEmail.text;
        string password = SignUpPassword.text;
        string cPassword = ConfirmPassword.text;

        if (password == cPassword)
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                
                if (task.IsCompleted && !task.IsFaulted)
                {
                    Debug.Log("Player registered successfully!");
                    SendVerificationEmail();
                    ClearSignUpFields();
                    ShowStatusPanel("Registration Successful! Please verify your email");
                    if (task.Result.User != null)
                    {
                        Debug.Log($"User created: {task.Result.User.Email}");
                    }
                    else
                    {
                        Debug.LogError("Current user is null after registration.");
                    }
                    
                }
                else
                {
                    HandleTaskException(task.Exception);
                }
            });
        }
        else
        {

            ShowStatusPanel("Passwords do not match. Please try again!!");
            Debug.Log("Passwords do not match. Please try again");
        }
    }
    public async void Login()
    {
        string email = LoginEmail.text;
        string password = Password.text;
        try
        {
            AuthResult authresult = await auth.SignInWithEmailAndPasswordAsync(email, password);
            FirebaseUser user = authresult.User;
            if (user.IsEmailVerified)
            {
                Debug.Log($"User logged in successfully: {user.Email}");
                ShowStatusPanel("Login Successful! Welcome");
                
                FetchUserdata(user.UserId);
                /*  SetStatus("Login Successful! Welcome.");
                  FetchUserData(user.UserId); // Fetch user data from the database*/
                SceneManager.LoadSceneAsync("HomePage");
               
            }
            else
            {
                
                await user.SendEmailVerificationAsync();
                ShowStatusPanel("Verification email sent");
                Debug.Log("Verification email sent.");

            }
        }
        catch (Exception ex)
        {
            {
                HandleException(ex);

            }
        }
    }
    public bool isEmailverified() {
        isEmailver = true;
        return true;
    }
    // Method to Handle Task Exceptions (General for Firebase Tasks)
    private void HandleTaskException(AggregateException exception)
    {
        foreach (var innerEx in exception.InnerExceptions)
        {
            if (innerEx is FirebaseException firebaseEx)
            {
                HandleFirebaseError(firebaseEx);
                return;
            }
        }
        Debug.LogError("Unknown error occurred.");
        Debug.Log("An error occurred. Please try again.");
    }


    public void ShowStatusPanel( string msg)
    {

        TextMeshProUGUI statusText = StatusPanel.GetComponentInChildren<TextMeshProUGUI>();
        statusText.text = msg;
        StatusPanel.SetActive(true); // Show the panel
        StartCoroutine(HideStatusPanelAfterDelay(4f)); // Call the coroutine to hide it after 4 seconds
    }

    // Coroutine to hide the status panel after a delay
    private IEnumerator HideStatusPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay (4 seconds)
        StatusPanel.SetActive(false);
    }
    private void ClearSignUpFields()
    {
        SignUpEmail.text = "";
        SignUpPassword.text = "";
        ConfirmPassword.text = "";
    }



    private void HandleFirebaseError(FirebaseException exception)
    {
        switch ((AuthError)exception.ErrorCode)
        {
            case AuthError.InvalidEmail:
                Debug.Log("Invalid email address format.");
                break;
            case AuthError.WrongPassword:
                Debug.Log("Incorrect password. Please try again.");
                break;
            case AuthError.UserNotFound:
                Debug.Log("No user found with this email.");
                break;
            case AuthError.NetworkRequestFailed:
                Debug.Log("Network error. Please check your connection.");
                break;
            default:
                Debug.Log($"Error: {exception.Message}");
                break;
        }
    }
    // Method to send a verification email to the user
    private void SendVerificationEmail()
    {
        FirebaseUser user = auth.CurrentUser;

        if (user != null)
        {
            user.SendEmailVerificationAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    Debug.Log("Verification email sent successfully.");
                   
                    
                        StartEmailVerificationCheck();
                    
                }
                else
                {
                    Debug.LogError("Failed to send verification email.");
                }
            });
            
        }
        else
        {
            Debug.LogError("No user is currently logged in.");
        }
    }
    private void HandleException(Exception ex)
    {
        if (ex is FirebaseException firebaseEx)
        {
            HandleFirebaseError(firebaseEx);
        }
        else
        {
            Debug.LogError($"An unknown error occurred: {ex.Message}");
            Debug.Log("An unknown error occurred. Please try again.");
        }
    }
    private void CreateNewUserData(string userId)
    {
        DataToSave userdata = new DataToSave
        {
            userId = userId,
            Username = Username.text, 
            Email = auth.CurrentUser.Email,
            CurrentLevel = 1,
            UnlockedLevel = 1,
            LivesRemaining = 5,
            TimeBreak = false,
        };

        string json = JsonUtility.ToJson(userdata);
        dbref.Child("users").Child(userId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("User data saved successfully in Firebase.");
            }
            else
            {
                Debug.LogError("Failed to save user data: " + task.Exception?.Message);
            }
        });

        DataManager.Instance.InitializeGameData(userdata.userId,userdata.Username,userdata.Email);
    }
    public void FetchUserdata(string userId)
    {
        StartCoroutine(FetchEnum(userId));
    }
    IEnumerator FetchEnum(string userID) {
        var serverData =dbref.Child("users").Child(userID).GetValueAsync();
        yield return new WaitUntil(predicate: ()=>serverData.IsCompleted);
        Debug.Log("Fetching completed");
        DataSnapshot snapshot=serverData.Result;
        string jsonData= snapshot.GetRawJsonValue();
        if (jsonData != null) {
             Debug.Log(jsonData);
           DataToSave dts=JsonUtility.FromJson<DataToSave>(jsonData);
            DataManager.Instance.UserId=dts.userId;
            DataManager.Instance.Username=dts.Username;
            DataManager.Instance.Email=dts.Email;
            DataManager.Instance.CurrentLevel = dts.CurrentLevel;
            DataManager.Instance.UnlockedLevel = dts.UnlockedLevel;
            DataManager.Instance.LivesRemaining = dts.LivesRemaining;
            DataManager.Instance.TimeBreak = dts.TimeBreak;
            
        }
        else
        {
            Debug.Log (" data is null");
        }
    }
    /*    IEnumerator WaitForEmailVerification(Task<AuthResult> task)
        {
            Debug.Log("Entered into waiting fn");
            FirebaseUser user = task.Result.User;
            if (user == null)
            {
                Debug.LogError("No current user found! Coroutine cannot proceed.");
                yield break;
            }

            Debug.Log($"Current user email: {user.Email}");

            while (user != null && !user.IsEmailVerified)
            {
                Debug.Log("Checking verification status...");
                Task reloadTask = user.ReloadAsync(); // Execute reload task outside of try block

                while (!reloadTask.IsCompleted) // Wait until task is completed
                {
                    yield return null;
                }

                if (reloadTask.IsFaulted)
                {
                    Debug.LogError("Error reloading user: " + reloadTask.Exception?.Message);
                    yield break;
                }

                yield return new WaitForSeconds(2); // Wait before rechecking
            }

            if (user.IsEmailVerified)
            {
                Debug.Log("User verified! Creating database entry...");
                CreateNewUserData(user.UserId);
            }
            else
            {
                Debug.LogError("User verification failed or user is null.");
            }
        }*/
    public async void StartEmailVerificationCheck()
    {
        FirebaseUser user = auth.CurrentUser;
        
        Debug.Log("StartEmailVerificationCheck");

        if (user == null)
        {
            Debug.LogError("No current user found! Email verification check cannot proceed.");
            return;
        }

        Debug.Log($"Started email verification check for: {user.Email}");

        while (user != null && !user.IsEmailVerified)
        {
            try
            {
                await user.ReloadAsync(); // Reload user data to check verification status
                Debug.Log("Waiting for email verification...");
                await Task.Delay(1000); // Wait for 1 second before checking again
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error reloading user data: {ex.Message}");
                return;
            }
        }

        if (user.IsEmailVerified)
        {
            Debug.Log("User verified! Creating database entry...");
            CreateNewUserData(user.UserId);
           
        }
        else
        {
            Debug.LogError("Email verification failed or user became null.");
        }
    }




}

class DataToSave
{
    public string userId;
    public string Username;
    public string Email;
    public int CurrentLevel;
    public int UnlockedLevel;
    public int LivesRemaining;
    public bool TimeBreak;

}