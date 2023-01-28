using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondMazeMode : MonoBehaviour
{
    private float stopWatch=0;

    private int size;

    private int keysNumber;
    float maxKeys;

    public float time = 120;

    public Text keysNumberText;
    public Text stopWatchText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        maxKeys = PlayerPrefs.GetInt("level") / 4;

        if (keysNumber == maxKeys)
        {
            keysNumberText.text = "Find the golden door";
        }
        else { keysNumberText.text = "Find the keys(" + keysNumber + "/" + maxKeys + ")"; }

        stopWatch += Time.deltaTime;
        stopWatchText.text = "Time past: " +(int)stopWatch;
    }


    public void EndWallCollision() //When the player goes through the golden door
    {   
        SceneManager.LoadScene("Maze Runer");
    }
    public void KeyCollision()//When the player goes through a key
    {
        keysNumber++;

        if (keysNumber == maxKeys)
        {

        }
    }
}
