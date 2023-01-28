using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMaze : MonoBehaviour
{
    public GameObject mazes;

    AudioClip[] clip;
    AudioSource source;

    public void CheckMoney()
    {
        source = GetComponent<AudioSource>();
        clip = GetComponent<ChooseSkin>().clip;
        source.clip = clip[0];
        source.Play();
        if (PlayerPrefs.GetFloat("Money") >= 75000)     
        { Unlock(); }
        else { Reject(); }
    }
    void Unlock()
    {
        for (int i = 0; i < mazes.GetComponent<MazeList>().mazes.Count; i++)
        {
            if (PlayerPrefs.GetString(i.ToString()+"Maze") != "Unlocked")
            {
                PlayerPrefs.SetString(i.ToString() + "Maze", "Unlocked");
                this.gameObject.GetComponent<ChooseSkin>().ChangeMaze(i);
                PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") - 75000);
                Debug.Log("New Maze: " + PlayerPrefs.GetFloat("Money"));
                break;
            }
        }

        source.clip = clip[1];
        source.Play();
    }

    void Reject()
    { }
}
