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
        
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

         UserNameInput.text = "demofirebase@gmail.com";
         PasswordInput.text = "abcdefgh";
        
        SignupButton.onClick.AddListener(() => Signup(UserNameInput.text, PasswordInput.text));
        LoginButton.onClick.AddListener(() => Login(UserNameInput.text, PasswordInput.text));
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
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                return;
            }
            
            FirebaseUser newUser = task.Result; // Firebase user has been created.
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
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                PlayerPrefs.SetInt("LoginSuccess", 0);
                SceneManager.LoadScene("LoginResults");
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
