using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class ChooseSkin : MonoBehaviour
{
    [SerializeField]
    [Header("Characters")]
    public Slider speedSlider;
    public Slider visionSlider;
    public Slider timeSlider;
    public GameObject charactersList;
    List<GameObject> allCharacters= new List<GameObject>();
    string changeDirectionCharacter;
    public GameObject  rightButtonCharacter, leftButtonCharacter;
    public GameObject skins;

    [SerializeField]
    [Header("Mazes")]
    public GameObject maze;
    public GameObject allMazes;
    List<Mazes> mazesClassList = new List<Mazes>();
    public GameObject randomColorsText;
    string changeDirectionMaze;
    public GameObject rightButtonMaze, leftButtonMaze;
    int choosenIdMaze;
    GameObject[] walls;
    GameObject[] floors;

    [SerializeField]
    [Header("Controls")]
    public Image locked;

    [SerializeField]
    [Header("HighScores")]
    public GameObject resultPrefab;
    public GameObject resultList;
    public GameObject loading;
    public GameObject personalResult;

    [SerializeField]
    [Header("Others")]
    private AudioSource source;
    public AudioClip[] clip;
    public GameObject[] getMoneyButtons;
    string currentMenu = "";

    // Start is called before the first frame update
    void Start()
    {

       walls = GameObject.FindGameObjectsWithTag("Wall");
        floors = GameObject.FindGameObjectsWithTag("Floor");

        GenerateCharacters();
        source = GetComponent<AudioSource>();
        ChooseMenu("Characters");

        ShowResults("total");
    }

    private void Update()
    {
        CharactersView();
        MazesView();

        if (PlayerPrefs.GetInt("RewardedLoaded") == 1)
        {
            switch (currentMenu)
            {
                case "Characters": getMoneyButtons[0].SetActive(true); break;
                case "Mazes": getMoneyButtons[1].SetActive(true); break;
            }
        }
        else 
        {
            switch (currentMenu)
            {
                case "Characters": getMoneyButtons[0].SetActive(false); break;
                case "Mazes": getMoneyButtons[1].SetActive(false); break;
            }
        }
       
    }


    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CHARACTERS MENU
    /// 

    void CharactersView()
    {
        if (charactersList.GetComponent<ChangeSkin>().closed)
        {
            if (changeDirectionCharacter == "Right")
            {
                changeDirectionCharacter = "";
                PlayerPrefs.SetInt("characterId", PlayerPrefs.GetInt("characterId") + 1);
                Debug.Log("Go right" + PlayerPrefs.GetInt("characterId"));
                ChangeCharacter(PlayerPrefs.GetInt("characterId"));
            }
            if (changeDirectionCharacter == "Left")
            {
                changeDirectionCharacter = "";
                PlayerPrefs.SetInt("characterId", PlayerPrefs.GetInt("characterId")-1);
                Debug.Log("Go left" + PlayerPrefs.GetInt("characterId"));
                ChangeCharacter(PlayerPrefs.GetInt("characterId"));
            }
        }
        charactersList.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }

    private void ResetCh()
    {
        for (int i = 0; i < skins.GetComponent<SkinList>().skins.Count; i++)
        {
            if (i == 0 || i == 1 || i == 2)
            {
                PlayerPrefs.SetString(i.ToString(), "Unlocked");
            }
      //     else { PlayerPrefs.SetString(i.ToString(), "Locked"); }
        }
    }

    void GenerateCharacters()
    {
        speedSlider.maxValue = 70;
        visionSlider.maxValue = 70;
        timeSlider.maxValue = 15;

    ResetCh();

        for (int i = 0; i < skins.GetComponent<SkinList>().skins.Count; i++)
        {
            if (PlayerPrefs.GetString(i.ToString()) == "Unlocked" || i == 0 || i == 1 || i == 2)
            {
                AddCharacter(i);
            }            
        }
        ChangeCharacter(PlayerPrefs.GetInt("characterId"));
    }

    public void AddCharacter(int id)
    {
        GameObject skin;
        skin = Instantiate(skins.GetComponent<SkinList>().skins[id].character, charactersList.transform.position, charactersList.transform.rotation);
        skin.name = id + "Skin";
        skin.transform.parent = charactersList.gameObject.transform;
        skins.GetComponent<SkinList>().skins[id].name = id.ToString();
        allCharacters.Add(skin);
    }

    public void ChangeCharacter(int id)
    {
        for (int i=0;i< skins.GetComponent<SkinList>().skins.Count;i++)
        {
            if (PlayerPrefs.GetString(i.ToString()) == "Unlocked")
            {
                allCharacters[i].SetActive(false);
            }
        }
        allCharacters[id].SetActive(true);

        speedSlider.value = skins.GetComponent<SkinList>().skins[id].speed;
        visionSlider.value = skins.GetComponent<SkinList>().skins[id].vision;
        timeSlider.value = skins.GetComponent<SkinList>().skins[id].time;

        PlayerPrefs.SetInt("characterId", id);

        if (skins.GetComponent<SkinList>().skins.Count == PlayerPrefs.GetInt("characterId") + 1)
        {
            rightButtonCharacter.SetActive(false);
            leftButtonCharacter.SetActive(true);
        }
        else if (PlayerPrefs.GetString((PlayerPrefs.GetInt("characterId") + 1).ToString())!="Unlocked")
        {
            rightButtonCharacter.SetActive(false);
            leftButtonCharacter.SetActive(true);
        }
        else if (0 == PlayerPrefs.GetInt("characterId"))
        {
            leftButtonCharacter.SetActive(false);
            rightButtonCharacter.SetActive(true);
        }
        else
        {
            rightButtonCharacter.SetActive(true);
            leftButtonCharacter.SetActive(true);
        }
    }

    public void ChangeId(string target)
    {
        if (charactersList.GetComponent<ChangeSkin>().readyToChange)
        { 
            source.clip = clip[0];
            source.Play(0);
            changeDirectionCharacter = target;
            charactersList.GetComponent<ChangeSkin>().CloseCharacter();
        }
    }




    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// MAZE MENU
    /// 

    void MazesView()
    {
        if (changeDirectionMaze == "Right")
        {
            changeDirectionMaze = "";
            choosenIdMaze++;
            ChangeMaze(choosenIdMaze);
        }
        if (changeDirectionMaze == "Left")
        {
            changeDirectionMaze = "";
            choosenIdMaze--;
            ChangeMaze(choosenIdMaze);
        }
    }

    void GenerateMaze()
    {
        mazesClassList = allMazes.GetComponent<MazeList>().mazes;
        choosenIdMaze = PlayerPrefs.GetInt("mazeId");

        for (int i = 0; i < mazesClassList.Count; i++)
        {
            if (i == 0 || i == 1 || i == 2)
            {
                PlayerPrefs.SetString(i.ToString() + "Maze", "Unlocked");
            }
        }
        ChangeMaze(choosenIdMaze);
    }

    public void ChangeMaze(int id)
    {
        Debug.Log("Choosen maze is: "+id);
        PlayerPrefs.SetInt("mazeId", id);
        choosenIdMaze = PlayerPrefs.GetInt("mazeId");

        if (id == 0)
        {
            rightButtonMaze.SetActive(true);
            leftButtonMaze.SetActive(false);
            maze.SetActive(true);
            randomColorsText.SetActive(false);
        }
        else if (PlayerPrefs.GetString((id + 1).ToString() + "Maze") != "Unlocked")
        {
            rightButtonMaze.SetActive(false);
            leftButtonMaze.SetActive(true);
            maze.SetActive(true);
            randomColorsText.SetActive(false);
        }
        else
        {
            rightButtonMaze.SetActive(true);
            leftButtonMaze.SetActive(true);
            maze.SetActive(true);
            randomColorsText.SetActive(false);
        }

        foreach (GameObject wall in walls)
        {
            wall.GetComponent<Renderer>().material.SetColor("_Color", mazesClassList[id].wallColor);
        }
        foreach (GameObject floor in floors)
        {
            floor.GetComponent<Renderer>().material.SetColor("_Color", mazesClassList[id].floorColor);
        }
        
    }

    public void ChangeMazeId(string target)
    {
        source.clip = clip[0];
        source.Play(0);
        changeDirectionMaze = target;
    }

    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// CONTROLS MENU

    public void LeftPosition()
    {
        source.Play();
        PlayerPrefs.SetString("Joystick Position", "Left"); 
    }
    public void RightPosition()
    {
        source.Play();
        PlayerPrefs.SetString("Joystick Position", "Right"); 
    }


    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// HIGHSCORE MENU

    public void ShowResults(string typeLeaderboard)
    {
        foreach (Transform child in resultList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Highscore high = new Highscore();
        high.arenaScore = PlayerPrefs.GetInt("LevelsMode");
        high.runnerScore = PlayerPrefs.GetInt("HighScore");
        high.total = high.arenaScore * high.runnerScore;
        string json = JsonUtility.ToJson(high);

        FirebaseDatabase.DefaultInstance.RootReference.Child("Highscore").Child(PlayerPrefs.GetString("Username")).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                FirebaseDatabase.DefaultInstance.GetReference("Highscore").OrderByChild(typeLeaderboard).GetValueAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCompleted)
                    {

                        DataSnapshot snapshot = task.Result;
                        int position = (int)snapshot.ChildrenCount;

                        foreach (DataSnapshot s in snapshot.Children)
                        {
                            if (position <= 100)
                            {
                                Debug.Log(s.Child("total").ToString());
                                //  Debug.Log(s.Key.ToString());
                                GenerateResults(position, s.Key.ToString(), s.Child("arenaScore").Value.ToString(), s.Child("runnerScore").Value.ToString(), s.Child("total").Value.ToString());
                            }
                            if (PlayerPrefs.GetString("Username") == s.Key.ToString())
                            {
                                CreateOwnResult(position, s.Child("arenaScore").Value.ToString(), s.Child("runnerScore").Value.ToString(), s.Child("total").Value.ToString());
                            }
                            position--;
                        }
                        loading.SetActive(false);
                    }
                });
            }
        });
    }



    void GenerateResults(int position,string name, string arena, string runer, string total)
    {
        GameObject result = Instantiate(resultPrefab, this.gameObject.transform.position,Quaternion.identity);
        result.transform.SetParent(resultList.transform,true);
        result.transform.localScale = resultPrefab.transform.localScale;
        result.transform.GetChild(0).GetComponent<Text>().text = position.ToString();
        result.transform.GetChild(1).GetComponent<Text>().text = name;
        result.transform.GetChild(2).GetComponent<Text>().text = arena;
        result.transform.GetChild(3).GetComponent<Text>().text = runer;
        result.transform.GetChild(4).GetComponent<Text>().text = total;      
    }

    void CreateOwnResult(int position, string arena, string runer, string total)
    {
        personalResult.transform.GetChild(0).GetComponent<Text>().text = position.ToString();
        personalResult.transform.GetChild(1).GetComponent<Text>().text ="ME";
        personalResult.transform.GetChild(2).GetComponent<Text>().text = arena;
        personalResult.transform.GetChild(3).GetComponent<Text>().text = runer;
        personalResult.transform.GetChild(4).GetComponent<Text>().text = total;
    }
        

    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Choose a Menu
    /// 

    public GameObject[] menus;
    public void ChooseMenu(string menu)
    {
        currentMenu = menu;

        source.clip = clip[0];
        source.Play();
        foreach (GameObject mn in menus)
        {
            mn.SetActive(false);
        }
        charactersList.SetActive(false);

        switch (menu)
        {
            case "Characters": menus[0].SetActive(true); charactersList.SetActive(true); maze.SetActive(false); break;
            case "Mazes": menus[1].SetActive(true); GenerateMaze(); break;
            case "Controls": menus[2].SetActive(true); maze.SetActive(false); break;
            case "Highscores": menus[3].SetActive(true); maze.SetActive(false); break;
        }
    }
}
