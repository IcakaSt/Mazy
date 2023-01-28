using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Photon.Pun;
using UnityEngine;

public class MultiplayerMovement : MonoBehaviour
{
    public GameObject skins;
    ParticleSystemRenderer part;

    [SerializeField] private float speed = 6.0F;
    [SerializeField] private float gravity = -9.81F;
    [SerializeField] private bool jump;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] float fallMultiplier = 2.5f, lowjumpMultiplier = 2f;
    public float MAX_SWIPE_TIME = 2.5f;
    public float MIN_SWIPE_DISTANCE = 0.5f;

    private CharacterController controller;

    [SerializeField] private float turningTime = 0.1f;
    [SerializeField] private float turningVelocity = 0.1f;

    [SerializeField] Vector3 velocity;
    GameObject[] moveJoysticks;
    VariableJoystick variableJoystick;

    GameObject playerSkin;

    public int time;

    public AudioSource audioData;
    public AudioSource audioData2;
    Vector3 changepos;

    public AudioClip[] clip;

    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        changepos = this.transform.position;

        audioData = GetComponent<AudioSource>();

        controller = GetComponent<CharacterController>();
        part = GetComponent<ParticleSystemRenderer>();

        var skinsClassList = skins.GetComponent<SkinList>().skins;

        Debug.Log("Index is :" + PlayerPrefs.GetInt("characterId"));
        speed = skinsClassList[PlayerPrefs.GetInt("characterId")].speed;
        time = skinsClassList[PlayerPrefs.GetInt("characterId")].time;
        playerSkin = Instantiate(skinsClassList[PlayerPrefs.GetInt("characterId")].character, this.gameObject.transform.position, Quaternion.identity);
        playerSkin.transform.parent = this.gameObject.transform;
        playerSkin.name = "Character";
        part.material = skinsClassList[PlayerPrefs.GetInt("characterId")].material;
        moveJoysticks = GameObject.FindGameObjectsWithTag("Controller");
        /*/
                if (PlayerPrefs.GetString("JoystickPosition") == "Right")
                {
                    moveJoysticks[1].SetActive(false);
                    variableJoystick = moveJoysticks[0].GetComponent<VariableJoystick>();
                }
                else
                {
                    moveJoysticks[0].SetActive(false);
                    variableJoystick = moveJoysticks[1].GetComponent<VariableJoystick>();
                }
        /*/

        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (playerSkin.gameObject.activeSelf)
            {
                //Move
                //  float horizontal = variableJoystick.Horizontal;
                // float vertical = variableJoystick.Vertical;

                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                if (horizontal != 0 || vertical != 0)
                {
                    float targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turningVelocity, turningTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    Vector3 directionMove = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                    if (!jump && !controller.isGrounded || controller.isGrounded || jump) { controller.Move(directionMove.normalized * speed * Time.deltaTime); }

                    Jumping();


                    if (Vector3.Distance(changepos, this.transform.position) > 0.3)
                    {
                        if (!audioData2.isPlaying)
                        {
                            audioData2.Play(0);
                        }
                        changepos = this.transform.position;
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Jumping();
            }
            //Death
            if (!playerSkin.activeSelf) { StartCoroutine(Death()); }
        }

    }

    IEnumerator Death()
    {
        audioData.clip = clip[1];
        if (!audioData.isPlaying) { audioData.Play(0); }

        yield return new WaitForSeconds(1);
        switch (SceneManager.GetActiveScene().name)
        {
            case "Arena":
                PlayerPrefs.SetString("Passed", "Failed");
                SceneManager.LoadScene("GameOverArena"); break;
            case "Maze Runer":
                SceneManager.LoadScene("GameOver"); break;
        }

    }

    void Jumping()
    {
        //Jump
        controller.Move(velocity * Time.deltaTime);
        if (jump && controller.isGrounded || Input.GetKeyDown("space") && controller.isGrounded)
        {
            audioData.clip = clip[0];
            audioData.Play(0);
            velocity = Vector3.up * jumpPower;
            jump = false;
        }
        velocity += Vector3.up * gravity * (fallMultiplier - 1) * Time.deltaTime;
        if (velocity.y > 0)
        {
            velocity += Vector3.up * gravity * (lowjumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Jump()
    {
        jump = true;
    }
}
