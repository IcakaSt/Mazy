using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject target;
    public GameObject skins;
    [SerializeField] float z = -70.1f;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       //   target = GameObject.Find("Player(Clone)");
            var skinsClassList = skins.GetComponent<SkinList>().skins;
            if (target)
            {
                transform.position = target.transform.position + new Vector3(0, skinsClassList[PlayerPrefs.GetInt("characterId")].vision, z);
            }
      
    }
}
