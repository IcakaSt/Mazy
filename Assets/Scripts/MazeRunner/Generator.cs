using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
public class Generator : MonoBehaviour
{
    public GameObject player;
    public GameObject floor;
    public GameObject wall;

    public float wallLength = 1;
    public int height = 5;
    public int width = 5;
    private Vector3 initPos;
    private GameObject wallHolder;

    public Cell[] cells;

    int visitedCells = 0;
    int totalCells;

    private int children;

    bool startedB = false;

    private List<int> lastCells;

    int chosenOne;
    int currCell;
    public List<int> wrongChoices;
    public List<int> wrongNeighbours;
    int neighbourNumber;

    public Cell[] neighbours = new Cell[5];
    int chooseNeigbhour;

    Color colorFloor, colorWall;
    public GameObject mazes;

    private void Start()
    {
     //   GenerateMaze(height, width);
    }

    public Cell[] GenerateMaze(int widthOfMaze, int heightOfMaze)
    {
        height = heightOfMaze;
        width = widthOfMaze;
        CreateWalls();
        return cells;
    }
    void CreateWalls()
    {
        colorFloor = mazes.GetComponent<MazeList>().mazes[PlayerPrefs.GetInt("mazeId")].floorColor;
        colorWall = mazes.GetComponent<MazeList>().mazes[PlayerPrefs.GetInt("mazeId")].wallColor;

        Debug.Log("Width: " + width + "; Height: " + height);

        initPos = new Vector3((-height / 2) + wallLength / 2, 0, (-width / 2) + wallLength / 2);
        Vector3 myPos = initPos;
        GameObject tempWall;

        wallHolder = new GameObject();
        wallHolder.name = "Maze";
        wallHolder.tag = "Maze";

        //for x axis
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j <= width; j++)
            {
                myPos = new Vector3(initPos.x + (j * wallLength) - wallLength / 2, 0, initPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;

                tempWall.GetComponent<Renderer>().material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
                tempWall.GetComponent<Renderer>().material.color = colorWall;
            }
        }

        //for y axis
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                myPos = new Vector3(initPos.x + (j * wallLength), 0, initPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;

                tempWall.GetComponent<Renderer>().material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
                tempWall.GetComponent<Renderer>().material.color = colorWall;
            }
        }
        CreateCells();
    }

    void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = height * width;
        GameObject[] allWalls;
        children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = new Cell[width * height];

        int eastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;

        //Gets all the children
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        //Assigns walls to the cells
        for (int cellpr = 0; cellpr < cells.Length; cellpr++)
        {
            if (termCount == width)
            {
                eastWestProcess++;
                termCount = 0;
            }

            cells[cellpr] = new Cell();
            cells[cellpr].west = allWalls[eastWestProcess];
            cells[cellpr].south = allWalls[childProcess + (width + 1) * height];

            eastWestProcess++;

            termCount++;
            childProcess++;
            cells[cellpr].east = allWalls[eastWestProcess];
            cells[cellpr].north = allWalls[childProcess + (width + 1) * height + width - 1];
        }
        totalCells = height * width;

        //Create floor
        for (int i = 0; i < totalCells; i++)
        {
            Vector3 floorPos = new Vector3(cells[i].north.transform.position.x, 0, cells[i].east.transform.position.z);
            GameObject tempFloor = Instantiate(floor, floorPos, Quaternion.identity) as GameObject;
            tempFloor.transform.parent = wallHolder.transform;

            tempFloor.GetComponent<Renderer>().material = new Material(Shader.Find("Legacy Shaders/Diffuse"));
            tempFloor.GetComponent<Renderer>().material.color = colorFloor;
            cells[i].floor = tempFloor;
        }

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(cells[0].floor.transform.position.x, 1, cells[0].floor.transform.position.z);
        Debug.Log("Player transported "+player.transform.position);
        player.GetComponent<CharacterController>().enabled = true;

        BuildMaze();
    }

    void BuildMaze()
    {
        currCell = 0;
        FindWay();
        startedB = true;
        Debug.Log("Building");
        while (startedB)
        {
            for (int i = totalCells - 1; i >= 0; i--)
            {
                if (!cells[i].visited)
                {
                    Neighbours(i);
                    if (neighbourNumber > 0)
                    {
                        chooseNeigbhour = Random.Range(1, neighbourNumber);
                        currCell = i;
                        DestroyWall(neighbours[neighbourNumber].neighbourWall);

                        FindWay();
                        break;
                    }
                }
            }
            if (visitedCells >= totalCells)
            {
                startedB = false;
            }
        }
        player.transform.position = new Vector3(cells[0].floor.transform.position.x, 1, cells[0].floor.transform.position.z);
    }

    void FindWay()
    {
        while (true)
        {
            ConditionsForMainWay();
            bool deadEnd = DeadEnd(wrongChoices);
            if (!deadEnd)
            {
                chosenOne = Random.Range(1, 5);
                while (wrongChoices.Contains(chosenOne))
                {
                    chosenOne = Random.Range(1, 5);
                }

                wrongChoices.Clear();
                cells[currCell].visited = true;
                visitedCells++;
                DestroyWall(chosenOne);
            }
            else
            {
                cells[currCell].visited = true;
                visitedCells++;
                wrongChoices.Clear();
                break;
            }
            GiveMeNextCell();
        }
    }
    bool DeadEnd(List<int> choice)
    {
        if (choice.Contains(1) && choice.Contains(2) && choice.Contains(3) && choice.Contains(4))
        { return true; }
        else { return false; }
    }

    void ConditionsForMainWay()
    {   //Up
        if (currCell + width >= width * height)
        { wrongChoices.Add(1); }
        else if (cells[currCell + width].visited)
        { wrongChoices.Add(1); }

        //Right
        if ((currCell + 1) % width == 0)
        { wrongChoices.Add(2); }
        else if (cells[currCell + 1].visited)
        { wrongChoices.Add(2); }

        //Left
        if (currCell % width == 0)
        { wrongChoices.Add(3); }
        else if (cells[currCell - 1].visited)
        { wrongChoices.Add(3); }

        //Down
        if (currCell < width)
        { wrongChoices.Add(4); }
        else if (cells[currCell - width].visited)
        { wrongChoices.Add(4); }
    }
    void GiveMeNextCell()
    {
        switch (chosenOne)
        {
            case 1: currCell = currCell + width; break;
            case 2: currCell = currCell + 1; break;
            case 3: currCell = currCell - 1; break;
            case 4: currCell = currCell - width; break;
        }
    }

    void DestroyWall(int choice)
    {
        switch (choice)
        {
            case 1: Destroy(cells[currCell].north); cells[currCell].north = null; break;
            case 2: Destroy(cells[currCell].east); cells[currCell].east = null; break;
            case 3: Destroy(cells[currCell].west); cells[currCell].west = null; break;
            case 4: Destroy(cells[currCell].south); cells[currCell].south = null; break;
        }
    }


    public Cell[] Neighbours(int index)
    {
        neighbourNumber = 0;
        //north
        if (index + width < totalCells)
        {
            if (cells[index + width].visited)
            {
                neighbours[neighbourNumber] = cells[index + width];
                neighbourNumber++;
                neighbours[neighbourNumber].neighbourWall = 1;
            }
        }

        //east
        if ((index + 1) % width != 0)
        {
            if (cells[index + 1].visited)
            {
                neighbours[neighbourNumber] = cells[index + 1];
                neighbourNumber++;
                neighbours[neighbourNumber].neighbourWall = 2;
            }
        }

        //west
        if (index % width != 0)
        {
            if (cells[index - 1].visited)
            {
                neighbours[neighbourNumber] = cells[index - 1];
                neighbourNumber++;
                neighbours[neighbourNumber].neighbourWall = 3;
            }
        }

        //south
        if (index >= width)
        {
            if (cells[index - width].visited)
            {
                neighbours[neighbourNumber] = cells[index - width];
                neighbourNumber++;
                neighbours[neighbourNumber].neighbourWall = 4;
            }
        }
        return neighbours;
    }
}
