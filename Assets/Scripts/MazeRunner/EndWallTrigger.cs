using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWallTrigger : MonoBehaviour
{
    Collider endWallCollider;

    // Start is called before the first frame update
    void Start()
    {
        endWallCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "EndWall")
        { endWallCollider.isTrigger = true; }
    }
}
