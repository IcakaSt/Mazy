using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HomeScreen");
    }
}
