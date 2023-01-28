using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    //public GameObject weapon;

    public Camera camera;
    public bool ray_hit_something = false;

     GameObject enemyPoint;

     GameObject currentHit;
    // Start is called before the first frame update
    void Start()
    {
        enemyPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(center);

        ray_hit_something = Physics.Raycast(ray, out hit);
     
        if (ray_hit_something)
        {
            currentHit = hit.transform.gameObject;

        //    weapon.transform.LookAt(hit.point);
        }
        if (currentHit!=null)
        {
            if (currentHit.gameObject.tag == "Enemy")
            {
                enemyPoint.SetActive(true);
            }
        }
        else { enemyPoint.SetActive(false); }
    }
}
