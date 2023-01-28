using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCharacter : MonoBehaviour
{
    public GameObject skins;

    AudioClip[] clip;
    AudioSource source;

    public void CheckMoney()
    {
        source = GetComponent<AudioSource>();
        clip = GetComponent<ChooseSkin>().clip;
        source.clip = clip[0];
        source.Play();
        if (PlayerPrefs.GetFloat("Money") >= 100000)
        { Unlock(); }
        else { Reject(); }
    }
    void Unlock()
    {
        source.clip = clip[1];
        source.Play();

        for (int i = 0; i < skins.GetComponent<SkinList>().skins.Count; i++)
        {
            if (PlayerPrefs.GetString(i.ToString()) != "Unlocked")
            {
                PlayerPrefs.SetString(i.ToString(),"Unlocked");
                this.gameObject.GetComponent<ChooseSkin>().AddCharacter(i);
                this.gameObject.GetComponent<ChooseSkin>().ChangeCharacter(i);
                PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") - 100000);
                break;
            }
        }    
    }

    void Reject()
    { }
}
