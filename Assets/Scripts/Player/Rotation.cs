using System.Collections;
using System.Collections.Generic;

using Vector3 = UnityEngine.Vector3;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Rotation : MonoBehaviour
{


    public Transform player;

    public float currentX = 0f;
    public float currentY = 0f;
   public float sesivityCamera = 1f;
    public float sesivityPlayer = 1f;

    private Touch initTouch = new Touch();

     float rotX;
     float rotY;
    float prevRotX=0;
    float prevRotY = 0;
    private Vector3 origRot;

    public float cinemachineXaxis=0;
    public float cinemachineYaxis=0;

    // Start is called before the first frame update
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotateX = Quaternion.Euler(0, currentX * sesivityPlayer, 0); //The rotation of the player
        player.Rotate(0, currentX * sesivityPlayer, 0);

   

}
private void LateUpdate()
    {
       
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2) //Beginning of the touch
            {
                initTouch = touch;

            }
            else if (touch.phase == TouchPhase.Moved && touch.position.x>Screen.width/2) //When the finger is moved
            {
                rotX = initTouch.position.x - touch.position.x;
                rotY = initTouch.position.y - touch.position.y;

               

                if (rotX != prevRotX)
                {
                    initTouch = touch;
                    currentX += -rotX; prevRotX = rotX; rotX = 0; 
                    
                }

                if (rotY != prevRotY)
                { currentY += rotY; prevRotY = rotY; rotY = 0; initTouch = touch; }

                if (currentY* sesivityCamera > 1)
                { currentY= 1/ sesivityCamera; }
                else if (currentY * sesivityCamera < 0)
                { currentY = 0; }

            }
            else if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2) //End of the touch
            { 
                initTouch = new Touch(); rotY = 0;rotX = 0;
                currentY = 0;
                currentX = 0;

            }
                                                              
        }
       
    }

}
