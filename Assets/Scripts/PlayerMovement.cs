using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CC;
    public float movementSpeed;
    public float startSpeed;
    public float boostSpeed;
    public float boostMax = 3;
    public float accumulatedBoostTime = 0f;
    public float powerupSpeed = 0;
    public bool boostable = true;
    public bool subtractSecond = false;
    public bool powerActive = false;
    private float rotationSensitivity;
    public Transform playerRotate;
    float XRotation = 0f;
    PhotonView PV;
    //public GameObject[] gunArray;
    //private Shoot shoot;
    //swag

    //public Image boostBar;

    public Vector3 movement;

    void Awake()
    {
        //PV = GetComponent<PhotonView>();
        PV = GetComponentInParent<PhotonView>();
    }

    public void Start()
    {

        movementSpeed = startSpeed;
        rotationSensitivity = 125;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!PV.IsMine)
        {
            Destroy(GetComponent<Camera>().gameObject);
        }
    }


    IEnumerator boostRecharge()
    {
        subtractSecond = true;

        yield return new WaitForSeconds(2);

        boostable = true;
        subtractSecond = false;
        //boostBar.fillAmount = 1;
    }


    void Look()
    {
        float Xaxis = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        float Yaxis = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

        XRotation -= Yaxis;
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        playerRotate.Rotate(Vector3.up * Xaxis);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float threed = Input.GetAxis("threed");

        Vector3 moveVector = (transform.right * horizontal) + (transform.forward * vertical) + (transform.up * threed);

        if (Input.GetKey(KeyCode.LeftShift) && boostable)
        {
            movementSpeed = boostSpeed;
            accumulatedBoostTime += Time.deltaTime;
            if (accumulatedBoostTime >= 3f)
            {
                boostable = false;
            }
        }
        else
        {

            //defaultParticle.Play();
            //boostParticle.Stop();
            movementSpeed = startSpeed + powerupSpeed;
        }


        if (!boostable && !subtractSecond)
        {
            StartCoroutine(boostRecharge());
            accumulatedBoostTime = 0;
        }

        /*if (PlayerAttack.abilityActivated)
        {
            StartCoroutine(SpecialPower());
        }*/

        movement = moveVector;
        CC.Move(moveVector * movementSpeed * Time.deltaTime);
    }

    void Update()
    {
        //if (!PV.IsMine)
        //return;
        if (!PV.IsMine)
            return;

        Look();
        Move();
        Debug.Log(PhotonNetwork.GetPing());
        if (Input.GetKey(KeyCode.Z))
        {
            Application.Quit();
        }
        }
    //}

}
