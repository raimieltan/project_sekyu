using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthSwitch : MonoBehaviour
{
    public void switchSignUp()
    {
        SceneManager.LoadScene("SIGN UP");
    }

    public void switchLogin()
    {
        SceneManager.LoadScene("LOG IN");
    }
}
