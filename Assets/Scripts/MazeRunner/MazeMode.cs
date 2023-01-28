using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MazeMode : MonoBehaviour
{
    public Slider slider;

    public Material yellowMaterial;

    int size = 0;
    
    [SerializeField]private int keysNumber;
    private int level = 0;

    private float maxKeys;

    public float time=60;

    public Text keysNumberText,addedTime,scoreText;

    public GameObject skins;
    public GameObject key, timeKey;

    Cell[] cells;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        time = PlayerPrefs.GetFloat("timeMazeRuner");
        slider.maxValue = 120;
        level = PlayerPrefs.GetInt("level");

        int width = Random.Range(2, level + 2);
        int height = Random.Range(2, level + 2);
        size = level + 5;
        cells = this.gameObject.GetComponent<Generator>().GenerateMaze(size, size);
        KeySpawn();

    }
    // Update is called once per frame
    void Update()
    {
        maxKeys = (level + 5) / 4;
        if (keysNumber == maxKeys) {keysNumberText.text = "Find the golden door";}
        else { keysNumberText.text = "Find the keys(" + keysNumber + "/" + maxKeys + ")"; }

        if (time > slider.maxValue) { slider.maxValue = time; }
        scoreText.text = "Score: " + level;
        time -= Time.deltaTime;
        Slider(time);
        if (time <= 0)
        {  
            PlayerPrefs.SetFloat("MoneyToAdd", CalculateMoney(PlayerPrefs.GetFloat("MoneyToAdd")));
            PlayerPrefs.SetInt("Score", level);
            SceneManager.LoadScene("GameOver"); 
        }
    }

    private float CalculateMoney(float addPrevious)
    {
        float money = addPrevious;
        money += level * 1000;
        return money;
    }

    void AddTime(float addTime)
    {
        addedTime.gameObject.SetActive(true);
        addedTime.text = "+" + (addTime);
        time += addTime;
        PlayerPrefs.SetFloat("timeMazeRuner", time);
        addedTime.GetComponent<AddedTime>().StartAnimation();
    }

    public void KeyCollision() //When the player goes through a key
    {
        audioData.Play(0);
        keysNumber++;
        if (keysNumber == maxKeys)
        {
            FindExit();
        }
    }

    void FindExit()
    {
        for (int i = 0; i < size*size; i++)
        {
            if (i + size > size * size || i - size < 0 || i % size == 0 || (i + 1) % size == 0)
            { cells[i].endWall = true; }
        }
        int choosenEndWall = Random.Range(0, size * size - 1);
        while (cells[choosenEndWall].endWall == false)
        {
            choosenEndWall = Random.Range(0, size * size - 1);
        }
        if (choosenEndWall + size >= size * size)
        {
            cells[choosenEndWall].north.GetComponent<MeshRenderer>().material = yellowMaterial;
            cells[choosenEndWall].north.tag = "EndWall";
        }
        else if (choosenEndWall - size < 0)
        {
            cells[choosenEndWall].south.GetComponent<MeshRenderer>().material = yellowMaterial;
            cells[choosenEndWall].south.tag = "EndWall";
        }
        else if (choosenEndWall % size == 0)
        {
            cells[choosenEndWall].west.GetComponent<MeshRenderer>().material = yellowMaterial;
            cells[choosenEndWall].west.tag = "EndWall";
        }
        else
        {
            cells[choosenEndWall].east.GetComponent<MeshRenderer>().material = yellowMaterial;
            cells[choosenEndWall].east.tag = "EndWall";
        }
    }

    public void TimeCollision() //When the player goes through a key
    {
        AddTime(15);
        audioData.Play(0);
    }

    public void EndWallCollision() //When the player goes through the golden door
    {
        level++;
        keysNumber = 0;
        PlayerPrefs.SetInt("MazeSize", level + 5);
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene("Maze Runer");
    }

    void KeySpawn()
    {
        for (int i = 0; i < size*size; i++)
        {
            cells[i].visited = false;
        }

        for (int i = 1; i <= size / 4; i++)
        {
            int choosenForKey = Random.Range(2, size * size);
            while (cells[choosenForKey].visited)
            { choosenForKey = Random.Range(2, size * size); }
            cells[choosenForKey].visited = true;
            Vector3 keyPos = new Vector3(cells[choosenForKey].floor.transform.position.x, 4, cells[choosenForKey].floor.transform.position.z);
            GameObject goldenKey = Instantiate(key, keyPos, Quaternion.identity) as GameObject;
        }

        if (timeKey != null)
        {
            int choosenForTimeKey = Random.Range(2, size * size);
            while (cells[choosenForTimeKey].visited)
            { choosenForTimeKey = Random.Range(2, size * size); }
            cells[choosenForTimeKey].visited = true;
            Vector3 timeKeyPos = new Vector3(cells[choosenForTimeKey].floor.transform.position.x, 4, cells[choosenForTimeKey].floor.transform.position.z);
            GameObject greenKey = Instantiate(timeKey, timeKeyPos, Quaternion.identity) as GameObject;
        }
    }

    void Slider(float value)
    {
        slider.value = value;

        Color color = new Color();
        if (value >= slider.maxValue/2) { color = new Color(0f, 1f, 0f); }
        else
        {
            if (value > slider.maxValue/4) {color = new Color(1f, 1f, 0f);}
            else { color = new Color(1f, 0f, 0f); }           
        }
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }
}
