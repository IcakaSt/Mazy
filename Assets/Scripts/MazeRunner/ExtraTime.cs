using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExtraTime : MonoBehaviour
{
   
    public void RestartGame()
    {
        PlayerPrefs.SetFloat("timeMazeRuner", 60);
        SceneManager.LoadScene("Maze Runer");
        PlayerPrefs.SetInt("Continue", 1);
    }
    private void Start()
    {
        PlayerPrefs.SetFloat("timeMazeRuner", 60);

        if (PlayerPrefs.GetInt("Continue") == 1)
        {
            this.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Continue", 0);
        }
    }
}
