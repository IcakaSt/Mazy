using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SetMazeSize : MonoBehaviour
{
    public InputField textBox;
    public Text errorM;
    int size;

    public bool good = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InputFieldClicked()
    { TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true); textBox.keyboardType = TouchScreenKeyboardType.NumberPad; }

    // Update is called once per frame
    void Update()
    {
      
     //   textBox.keyboardType = TouchScreenKeyboardType.NumberPad;
    }
  
    public void StartMaze()//Start the maze from the choose size scene
    {
        bool error = false;
        try
        {
            size = int.Parse(textBox.text);
        }
        catch { error = true; }

        if (textBox.text == "" || int.Parse(textBox.text) < 5)
        { error = true; }

        if (!error)
        {
            PlayerPrefs.SetInt("level", size - 4);
            SceneManager.LoadScene("MazesecondMode");
        }
        else { errorM.text = ("Enter valid size"); }
    }
}
