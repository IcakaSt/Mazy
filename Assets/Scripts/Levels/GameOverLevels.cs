using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine;

public class GameOverLevels : MonoBehaviour
{
    public Text PassedText,RestartOrNextButton;

    Highscore high = new Highscore();

    // Start is called before the first frame update
    void Start()
    {
        PassedText.text = PlayerPrefs.GetString("Passed");
        
        if (PlayerPrefs.GetString("Passed") == "Passed") 
        {
            PassedText.text = "Level Passed";
            RestartOrNextButton.text = "Next";
        }
        else
        {
            PassedText.text = "Level Failed";
            RestartOrNextButton.text = "Restart";
        }

        high.arenaScore = PlayerPrefs.GetInt("LevelsMode");
        high.runnerScore = PlayerPrefs.GetInt("HighScore");
        high.total = high.arenaScore * high.runnerScore;
        string json = JsonUtility.ToJson(high);
        Debug.Log(json);
        FirebaseDatabase.DefaultInstance.RootReference.Child("Highscore").Child(PlayerPrefs.GetString("Username")).SetRawJsonValueAsync(json);
    }
}

