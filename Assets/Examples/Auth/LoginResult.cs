using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginResult : MonoBehaviour {
    public Text LoginResultText;
	// Use this for initialization
	void Start () {

        int result = PlayerPrefs.GetInt("LoginSuccess", 0);
        LoginResultText.text = "Login Fails";
        if (result > 0)
            LoginResultText.text = "Login Success";



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
