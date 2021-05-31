using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public int levelID;
    public string levelName;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("SFX").GetComponent<AudioManager>();
    }

    public void SelectLevelById()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene(levelID);
    }

    public void SelectLevelByName()
    {
        audioManager.ButtonPressAudio();
        SceneManager.LoadScene(levelName);
    }
}
