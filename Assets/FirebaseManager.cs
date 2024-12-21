using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{ 
    //Firebase declarations
    public static FirebaseAuth auth;

    //Login GameObjects
    public TMP_InputField LoginEmail;
    public TMP_InputField Password;

    //SignUp GameObjects
    public TMP_InputField SignUpEmail;
    public TMP_InputField SignUpPassword;
    public TMP_InputField ConfirmPassword;
    public TMP_InputField Username;

    public GameObject StatusPanel;

    private void Awake()
    {
        auth=FirebaseAuth.DefaultInstance;

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
                    //CreateUserDatabaseEntry(email); 
                    SendVerificationEmail();
                    ClearSignUpFields();
                    /*  SetStatus("Registration Successful! Please verify your email.");*/
                    ShowStatusPanel("Registration Successful! Please verify your email");
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

}
    
