using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthBar healthBar;

    int maxHealth = 100;
    public int health;

    public float coolDown=2f;
    public bool takeDamage = false;

    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);
    } 
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Health" && health<100)
        { health += 10; }

       
    }
}
