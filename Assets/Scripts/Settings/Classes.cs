using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class Skins
{
    public string name;
    public int vision;
    public int speed;
    public int time;
    public Material material;
    public GameObject character;
}
[System.Serializable]
public class Cell
{
    public bool visited;
    public bool endWall;
    public GameObject north;//1
    public GameObject east;//2
    public GameObject west;//3
    public GameObject south;//4
    public GameObject floor;
    public int neighbourWall;
}

[System.Serializable]
public class Mazes
{
    public Color wallColor;
    public Color floorColor;
    public bool unlocked;
}

[System.Serializable]
public class Highscore
{
    public int arenaScore;
    public int runnerScore;
    public int total;
}

[System.Serializable]
public class Room
{
    public string code;
    public string type;
    public bool publicRoom;

    public void GenerateCode(int lettersCount)
    {
        string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < lettersCount; i++)
        {
            char c = st[Random.Range(0, st.Length)];
            code = code + c;
        }
        Debug.Log(code);
    }
}


