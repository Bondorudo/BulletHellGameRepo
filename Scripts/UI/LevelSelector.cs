using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int levelButtonCount;
    [SerializeField] private Transform content;
    [SerializeField] private Button buttonPrefab;

    private Button[] levelButtons;
    private string[] levelNames;
    private int i;

    private void Start()
    {
        // Setup arrays
        levelButtons = new Button[levelButtonCount];
        levelNames = new string[levelButtonCount];
        // Get the highest level player has reached in game
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        // Loop trough buttons array
        for (i = 0; i < levelButtons.Length; i++)
        {
            int a = i + 1;
            // Populate level names array with "Level_" + button id which should have a level that corresponds to the id in name
            levelNames[i] = "Level_" + a;
            // Instantiate new button
            Button newButton = Instantiate(buttonPrefab, content, false);
            // Set buttons text to level number
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = a.ToString();
            string copy = levelNames[i];
            // Give the button an onClick event so that the button takes player to the correct level
            newButton.GetComponent<Button>().onClick.AddListener(delegate { Select_Level(copy); });
            levelButtons[i] = newButton;

            // if button id is higher than highest level reached then disable that button
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    // Function to select a level takes a string level name parameter
    void Select_Level(string levelToLoad)
    {
        // Find audio manager and play button press audio
        // Find scene fader and fade to scene with parameter's name
        AudioManager.instance.PlaySound("UI_Button");
        FindObjectOfType<SceneFader>().FadeTo(levelToLoad);
    }
}
