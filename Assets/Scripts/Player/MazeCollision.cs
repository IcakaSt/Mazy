using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeCollision : MonoBehaviour
{
    GameObject mazeBuilder;
   
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        mazeBuilder = GameObject.FindGameObjectWithTag("MazeBuilder");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EndWall")
        {
            mazeBuilder.gameObject.GetComponent<MazeMode>().EndWallCollision(); 
        }
        if (collision.gameObject.tag == "Death")
        {
            GameObject.Find("Character").SetActive(false);
        }
    }
}
