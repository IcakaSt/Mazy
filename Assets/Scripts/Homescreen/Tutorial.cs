using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] icons;

    public Text tutorialText;
    public GameObject tutorial;
    public GameObject backButton;

    int next = 1;
    bool help = false;
    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(false);
        NextTutorial();
    }

    private void Update()
    {
        if (next > 1)
        {
            backButton.SetActive(true);
        }
        else { backButton.SetActive(false); }
    }

    void NextTutorial()
    {
        if (PlayerPrefs.GetInt("HomeScreenTutorial") == 0)
        {
            tutorial.SetActive(true);

            switch (next)
            {
                case 1: tutorialText.text = "UPDATES: Now you can check the world leaderboard in:"; ChangeIcon(icons[1]); break;
                case 2: tutorialText.text = "You can choose between two modes to play:"; ChangeIcon(icons[0]); break;
                case 3: tutorialText.text = "To change your CHARACTER, MAZE DESIGN, CONTROLS and check the LEADERBOARD go to:"; ChangeIcon(icons[1]); break;
                case 4: tutorialText.text = "ENJOY!"; Clear(); break;
                case 5: tutorial.SetActive(false); PlayerPrefs.SetInt("HomeScreenTutorial", 1); next = 1; break;
            }
        }
    }

    void Clear()
    {
        foreach (GameObject go in icons)
        {
            go.SetActive(false);
        }
    }

    void ChangeIcon(GameObject icon)
    {
        Clear();
        icon.SetActive(true);
    }

    public void Next()
    {
        next++;
        NextTutorial();
    }
    public void Back()
    {
        next--;
        NextTutorial();
    }

    void ShowTutorial()
    { if (help) { help = false; PlayerPrefs.SetInt("HomeScreenTutorial", 0); } NextTutorial(); }

    public void Help() { if (!tutorial.activeSelf) { help = true; ShowTutorial(); } }
}
