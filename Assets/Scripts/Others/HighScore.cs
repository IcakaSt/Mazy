using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using UnityEngine.Animations;

public class HighScore : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    private Touch initTouch = new Touch();

    private Animator anim;

    Highscore high = new Highscore();

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("Countinue") == 1)
        {
            continueButton.SetActive(false);
            PlayerPrefs.SetInt("Countinue", 0);
        }

        scoreText.text = PlayerPrefs.GetInt("Score").ToString();

        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
        { 
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));

            high.arenaScore = PlayerPrefs.GetInt("LevelsMode");
            high.runnerScore = PlayerPrefs.GetInt("HighScore");
            high.total = high.arenaScore * high.runnerScore;
            string json = JsonUtility.ToJson(high);
            Debug.Log(json);
            FirebaseDatabase.DefaultInstance.RootReference.Child("Highscore").Child(PlayerPrefs.GetString("Username")).SetRawJsonValueAsync(json);
        }
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        anim = GetComponent<Animator>();       
    }

    public void GoToHomecreen()
    {
        SceneManager.LoadScene("Homescreen");
    }
    public void StopAnimation()
    {
        anim.SetBool("ReverseGameOverAnimation", false);
    }
}
