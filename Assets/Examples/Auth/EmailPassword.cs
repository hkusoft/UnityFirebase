using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmailPassword : MonoBehaviour
{

    private FirebaseAuth auth;

    public InputField UserNameInput, PasswordInput;
    public Button SignupButton, LoginButton;


    // Use this for initialization
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        string email = UserNameInput.text = "voicecad@foxmail.com";
        string password = PasswordInput.text = "abcdefgh";

        SignupButton.onClick.AddListener(() => Signup(email, password));
        LoginButton.onClick.AddListener(() => Login(email, password));
    }


    public void Signup(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            //Error handling
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

    }

    public void Login(string email, string password)
    {        
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            PlayerPrefs.SetInt("LoginSuccess", user != null ? 1 : 0);
            SceneManager.LoadScene("LoginResults");
        });
    }
}
