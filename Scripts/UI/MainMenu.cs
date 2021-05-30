using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // Set time scale to 1 just incase.
        Time.timeScale = 1;
    }

    // Find scene fader and fade to specified scene.
    public void Play(string sceneToLoad)
    {
        FindObjectOfType<SceneFader>().FadeTo(sceneToLoad);
    }

    // Quit the application
    public void Quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
