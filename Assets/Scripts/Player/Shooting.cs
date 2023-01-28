using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    Animator anim;

    public float bullets;
    public Text bulletsN;

    public GameObject bullet;
    public Transform shootingP;
  //  public JoyButton shoot;
  //  public JoyButton secondShoot;
  //  private JoyButton shootButtonChecker;

    public GameObject weapon;
   public float maxDistance;

    public bool shootAble=true;

    public float rotationZ;
    public float rotationY;

    // Start is called before the first frame update
    void Start()
    {
       

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletsN.text = bullets.ToString();
        if (shootAble && bullets > 0) // Checking if there are any bullets
        {
            /*/   if (secondShoot.Pressed)
               {
                   Shoot();
                   shootButtonChecker = secondShoot;


               }
               if (shoot.Pressed)
               {
                   Shoot();
                   shootButtonChecker = shoot;
               }
              /*/
        }

    }  
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ammo")
        { bullets += 10; }
    }

    void Shoot()
    {
        shootAble = false;

        Instantiate(bullet, shootingP.position, shootingP.rotation);

        anim.SetBool("ShootAble", true);

    }
    public void ShootCoolDown()
    {
        anim.SetBool("ShootAble", false);
        shootAble = true; //Disable the weapon to be automatic
            
        
    }
}
