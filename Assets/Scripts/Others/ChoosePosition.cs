using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePosition : MonoBehaviour
{
    public GameObject LeftFrame;
    public GameObject RightFrame;
    // Start is called before the first frame update

    private void Update()
    {
        if (PlayerPrefs.GetString("JoystickPosition") == "Right")
        {
            RightFrame.SetActive(true);
            LeftFrame.SetActive(false);
        }
        else
        {
            LeftFrame.SetActive(true);
            RightFrame.SetActive(false);
        }
    }
    public void ControllerPosition(string position)
    { PlayerPrefs.SetString("JoystickPosition", position); }
}
