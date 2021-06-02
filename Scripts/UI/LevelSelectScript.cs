using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public int levelID;
    public string levelName;

    public void SelectLevelById()
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        SceneManager.LoadScene(levelID);
    }

    public void SelectLevelByName()
    {
        AudioManager.instance.PlaySound("UI_Buttons");
        SceneManager.LoadScene(levelName);
    }
}
