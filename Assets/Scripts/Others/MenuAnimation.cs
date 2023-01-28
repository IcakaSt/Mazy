using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{

    private string scenename="";
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StopAnimation()
    {
        anim.SetBool("StartMenu", false);
    }

    public void StopReverseAnimaion()
    {
        anim.SetBool("ExitMenu", false);
       
       // GetComponent<SceneChanger>().ChangeScene(scenename);
    }

    public void GetNewSceneName(string name)
    {
        scenename = name;
        anim.SetBool("ExitMenu", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
