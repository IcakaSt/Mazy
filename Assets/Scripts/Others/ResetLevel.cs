using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    public GameObject skins;
    public GameObject lockedSkins;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetFloat("timeMazeRuner", 120);
      //  PlayerPrefs.SetFloat("Money", 5000000);

        
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Username")))
        {
            SceneManager.LoadScene("Set Nickname");
        }

        foreach (Skins skin in lockedSkins.GetComponent<SkinList>().skins)
            {
                if (PlayerPrefs.GetString(skin.name)== "Unlocked")
                { skins.GetComponent<SkinList>().skins.Add(skin);
                lockedSkins.GetComponent<SkinList>().skins.Remove(skin); }
            }    
    }
}
