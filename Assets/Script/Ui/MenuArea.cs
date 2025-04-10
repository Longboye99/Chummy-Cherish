using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuArea : MonoBehaviour
{
    public void StartButton()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(1);
    }
}
