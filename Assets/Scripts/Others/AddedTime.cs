using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class AddedTime : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        anim.SetBool("StartAddedTime", true);
    }

    public void EndAddedTime()
    {
       // this.gameObject.SetActive(false);
        anim.SetBool("StartAddedTime", false);
    }
}
