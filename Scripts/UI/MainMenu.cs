using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1;
    }


    public void Play(string sceneToLoad)
    {
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
