using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    Animator anim;
    public bool readyToChange;
    public bool closed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenCharacter()
    {
        anim.SetBool("Opening", true);
        anim.SetBool("Closing", false);
        closed = true;
        readyToChange = false;
    }
    public void CloseCharacter()
    {
        closed = false;
        anim.SetBool("Closing", true);
        anim.SetBool("Opening", false);
        readyToChange = false;
    }
    public void ReadyToChange()
    {
        closed = false;
        readyToChange = true;
    }

}
