using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelsMode : MonoBehaviour
{
    [SerializeField]
    [Header("Destroying cells")]
    private float delay = 5;
    List<int> floorsId = new List<int>();
    List<GameObject> floors = new List<GameObject>();
    public List<GameObject> walls = new List<GameObject>();
    [SerializeField] bool destroyMaze = true;

    [SerializeField]
    [Header("Canvas")]
    int level=0;
    [SerializeField] private float time=5;
    [SerializeField] private float wholeTime = 0;
    public Text levelText;
    public Slider slider;

    [SerializeField]
    [Header("Game over")]

    public int width=2,height=3;

    public GameObject key;
    public GameObject player;

    Cell[] cells;
    AudioSource audioData;

    private void Start()
    {
      

        audioData = GetComponent<AudioSource>();
        time = player.GetComponent<Movement>().time * 15 + PlayerPrefs.GetInt("LevelsMode") / 3 * 20;

        if (PlayerPrefs.GetInt("LevelsMode") == 0) { width = 2; height = 2; }
        else
        {
            level = PlayerPrefs.GetInt("LevelsMode");
            width = Random.Range(1 + level, level + 5);
            height = Random.Range(1 + level, level + 3);
        }
        cells = this.gameObject.GetComponent<Generator>().GenerateMaze(width, height);

        slider.maxValue = time * 2;
        levelText.text = "Level: " + PlayerPrefs.GetInt("LevelsMode");
        for (int i = 0; i < cells.Length; i++)
        {
            if ((i / 10) % 2 == 0)
            {
                if (i % 2 != 0)
                { floorsId.Add(i); }
            }
            else
            {
                if (i % 2 == 0)
                { floorsId.Add(i); }
            }
            floors.Add(cells[i].floor);

            if (cells[i].north != null && !walls.Contains(cells[i].north)) { walls.Add(cells[i].north); }
            if (cells[i].south != null && !walls.Contains(cells[i].south)) { walls.Add(cells[i].south); }
            if (cells[i].east != null && !walls.Contains(cells[i].east)) { walls.Add(cells[i].east); }
            if (cells[i].west != null && !walls.Contains(cells[i].west)) { walls.Add(cells[i].west); }
        }
        KeySpawn();
    }

    private void Update()
    {
        Slider(time);

        wholeTime += Time.deltaTime;

        if (time > 0) { time -= Time.deltaTime; }

        if (time <= 0) { delay = 0; time = 0; } //Change delay between destroying cells
        else if (time <= 2) { delay = 0; }
        else if (time < slider.maxValue / 8) { delay = 1; }
        else if (time < slider.maxValue / 4) { delay = 3; }
        else if (time < slider.maxValue / 2) { delay = 7; }
        else { delay = 15; }
        try { if (destroyMaze) { StartCoroutine(DestroyingCells()); } }

        catch { return; }
        
    }
    public void KeyCollision() //When the player goes through a key
    {
        audioData.Play(0);
        time += slider.maxValue/5;
        if (time >= slider.maxValue) { NextLevel(); } else { KeySpawn(); }
    }

    void KeySpawn()
    {
        int choosenForKey = Random.Range(1, floors.Count);
        Vector3 keyPos = new Vector3(floors[choosenForKey].transform.position.x, 4, floors[choosenForKey].transform.position.z);
        GameObject goldenKey = Instantiate(key, keyPos, Quaternion.identity) as GameObject;     
    }

    public void NextLevel()
    {
        level++;
        PlayerPrefs.SetInt("LevelsMode", PlayerPrefs.GetInt("LevelsMode") + 1);
        PlayerPrefs.SetString("Passed", "Passed");
        PlayerPrefs.SetFloat("MoneyToAdd", CalculateMoney());
        SceneManager.LoadScene("GameOverArena");
    }

    private float CalculateMoney()
    {
        float money = 5f;
        wholeTime = 120 - wholeTime;
        money += level * 1000 + (int)wholeTime * 10;
        return money;
    }

    IEnumerator DestroyingCells()
    {
        destroyMaze = false;
        switch (Random.Range(1, 4))
        {
            case 1: StartCoroutine(DestroyFloor()); break;
            case 2: StartCoroutine(DestroyWall()); break;
            case 3: StartCoroutine(DestroyWall()); break;
        }
        yield return new WaitForSeconds(delay);
        destroyMaze = true;
    }

    IEnumerator DestroyWall()
    {
        if (walls.Count > 0)
        {
            int choosenWall = Random.Range(0, walls.Count);
            GameObject thisWall = walls[choosenWall];

            if (walls[choosenWall] != null)
            {
                thisWall.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
                walls.Remove(thisWall);
                yield return new WaitForSeconds(2);
                thisWall.GetComponent<BoxCollider>().isTrigger = true;
                thisWall.AddComponent<Rigidbody>().useGravity = true;
                yield return new WaitForSeconds(10);
                Destroy(thisWall); thisWall = null;
            }
            else { walls.Remove(thisWall); }
        }
    }

    IEnumerator DestroyFloor()
    {
        if (floors.Count > 0)
        {
            GameObject thisFloor = new GameObject();
            Destroy(thisFloor);

            int choosenSell = 0;
            if (floorsId.Count > 0)
            {
                choosenSell = floorsId[Random.Range(0, floorsId.Count)];
                floorsId.Remove(choosenSell);
                floors.Remove(cells[choosenSell].floor);
                thisFloor = cells[choosenSell].floor;
            }
            else { choosenSell = Random.Range(0, floors.Count); thisFloor = floors[choosenSell]; floors.Remove(thisFloor); }

            thisFloor.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
            yield return new WaitForSeconds(2);
            thisFloor.GetComponent<BoxCollider>().isTrigger = true;
            thisFloor.AddComponent<Rigidbody>().useGravity = true;
            yield return new WaitForSeconds(10);
            Destroy(thisFloor); thisFloor = null;
        }
    }


    void Slider(float value)
    {
        slider.value = value;
        Color color = new Color();
        if (value >= slider.maxValue / 4) { color = new Color(0f, 1f, 0f); }
        else
        {
            if (value > slider.maxValue / 8) { color = new Color(1f, 1f, 0f); }
            else { color = new Color(1f, 0f, 0f); }
        }
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }
}
