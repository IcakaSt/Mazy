using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeScenes(string scenename)
    {
        StartCoroutine(PlaySound(scenename));
    }

    IEnumerator PlaySound(string scenename)
    {
        source.Play();
        yield return new WaitWhile(() => source.isPlaying);

        Debug.Log(scenename);
        SceneManager.LoadScene(scenename);
    }
}
