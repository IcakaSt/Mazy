using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    AudioSource source;
    private Touch initTouch = new Touch();

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeScene(string scenename)
    {
        StartCoroutine(PlaySound(scenename));      
    }
    IEnumerator PlaySound(string scenename)
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);

        if (SceneManager.GetActiveScene().name == "ReadyToStart" || scenename == "Settings" || scenename == "Homescreen")
        {
            SceneManager.LoadScene(scenename);
        }
        else
        {
            PlayerPrefs.SetString("NextScene", scenename);
            SceneManager.LoadScene("ReadyToStart");
        }
    }



    public void ApplicationQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        /*/ foreach (Touch touch in Input.touches)
         {
             if (touch.phase == TouchPhase.Began && SceneManager.GetActiveScene().name == "ReadyToStart") //Beginning of the touch
             {
                 initTouch = touch;
             }
             else if (touch.phase == TouchPhase.Ended && SceneManager.GetActiveScene().name == "ReadyToStart") //End of the touch
             {
                 initTouch = new Touch();

                 if (PlayerPrefs.GetInt("Restarted") == 0)
                 {
                     PlayerPrefs.SetFloat("timeMazeRuner", 120);
                 }

                 ChangeScene(PlayerPrefs.GetString("NextScene"));
             }
         }
         /*/
    }

    public void StartGame()
    {
        ChangeScene(PlayerPrefs.GetString("NextScene"));
    }
}

