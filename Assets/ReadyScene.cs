using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReadyScene : MonoBehaviour
{
    public GameObject jumping;

    public GameObject[] mazerunClips;
    public GameObject[] arenaClips;
    GameObject middleClip;

    public Text tutorialText;
    public GameObject tutorial;
    public GameObject backButton;

    int next = 1;
    bool help = false;
    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(false);
        ShowTutorial();
    }

    private void Update()
    {
        if (next > 1)
        {
            backButton.SetActive(true);
        }
        else { backButton.SetActive(false); }
    }

    void FirstTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial1") == 0)
        {
            tutorial.SetActive(true);

            switch (next)
            {
                case 1: tutorialText.text = "You have 2 minutes to survive as many levels as possible!"; ChangeClip(mazerunClips[0]); break;
                case 2: tutorialText.text = "To complete a level collect all the needed keys to unlock the golden door!"; ChangeClip(mazerunClips[0]); break;
                case 3: tutorialText.text = "Find the golden door and go through it!"; ChangeClip(mazerunClips[1]); break;
                case 4: tutorialText.text = "Also, you can collect the green plus for extra time!"; ChangeClip(mazerunClips[2]); break;
                case 5: tutorialText.text = "Be a better version of yourself and beat your highscore!"; ChangeClip(mazerunClips[2]); break;
                case 6: tutorial.SetActive(false); PlayerPrefs.SetInt("Tutorial1", 1); next = 1; break;
            }
        }
    }

    void ChangeClip(GameObject clip)
    {
        
        if (middleClip != clip)
        {
            switch (PlayerPrefs.GetString("NextScene"))
            {
                case "Maze Runer":
                    foreach (GameObject go in mazerunClips)
                    {
                        go.SetActive(false);
                    }
                    break;
                case "Arena":
                    foreach (GameObject go in arenaClips)
                    {
                        go.SetActive(false);
                    }
                    break;
            }
            
           clip.SetActive(true);
           middleClip = clip;
        }
    }

    void SecondTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial2") == 0)
        {
            tutorial.SetActive(true);
            switch (next)
            {
                case 1: tutorialText.text = "Survive the level!"; ChangeClip(arenaClips[0]); break;
                case 2: tutorialText.text = "Pass the level by filling the bar with the time by collecting keys!"; ChangeClip(arenaClips[0]); break;
                case 3: tutorialText.text = "Be careful! The maze is self-destructing!"; ChangeClip(arenaClips[1]); break;
                case 4: tutorial.SetActive(false); PlayerPrefs.SetInt("Tutorial2", 1); next = 1; break;
            }

        }
    }

    public void ShowTutorial()
    {
        switch (PlayerPrefs.GetString("NextScene"))
        {
            case "Maze Runer":  jumping.SetActive(false);   if (help) { help = false; PlayerPrefs.SetInt("Tutorial1", 0); } FirstTutorial(); break;
            case "Arena":  jumping.SetActive(true); if (help) { help = false; PlayerPrefs.SetInt("Tutorial2", 0); } SecondTutorial(); break;
        }
    }

    public void Next()
    { 
        next++;
        ShowTutorial();
    }
    public void Back()
    {
        next--;
        ShowTutorial();
    }

    public void Help() { if (!tutorial.activeSelf) { help = true; } ShowTutorial(); }
}
