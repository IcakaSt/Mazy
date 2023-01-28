using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Coin : MonoBehaviour
{
    Animator anim;
    GameObject mazeBuilder;

    // Start is called before the first frame update
    void Start()
    {
        mazeBuilder = GameObject.FindGameObjectWithTag("MazeBuilder");
        anim = GetComponent<Animator>();

        if (this.gameObject.tag == "Time")
        {
            transform.Rotate(new Vector3(0, 0, -20));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Key")
        { transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); }
        if (this.gameObject.tag == "Time")
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime,Space.World);
        }
    }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                anim.SetBool("KeyDestroy", true);
            }        
        }

        public void DestroyKey()
        {
            anim.SetBool("KeyDestroy", false);
            this.gameObject.SetActive(false);

            if (this.gameObject.tag == "Time") { mazeBuilder.gameObject.GetComponent<MazeMode>().TimeCollision(); }
            else if (SceneManager.GetActiveScene().name == "Maze Runer") { Debug.Log("Key collected"); mazeBuilder.gameObject.GetComponent<MazeMode>().KeyCollision(); }
    
            if (SceneManager.GetActiveScene().name == "Arena") { mazeBuilder.gameObject.GetComponent<LevelsMode>().KeyCollision(); }
    }
}
