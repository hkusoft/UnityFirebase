using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginResult : MonoBehaviour
{

    public Text LoginResultText;
    void Start()
    {
        var user = PlayerPrefs.GetString("LoginUser", "Unknown");
        LoginResultText.text = "Successfully signed in as " + user;
        Debug.LogFormat("Successfully signed in as {0}", user);
    }
}
