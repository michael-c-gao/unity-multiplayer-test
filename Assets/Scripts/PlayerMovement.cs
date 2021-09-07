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
    private float rotationSensitivity;
    public Transform playerRotate;
    float XRotation = 0f;
    PhotonView PV;


    //public Vector3 movement;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Start()
    {

        movementSpeed = startSpeed;
        rotationSensitivity = 125.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
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

        //movement = moveVector;
        CC.Move(moveVector * movementSpeed * Time.deltaTime);
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
        else if (PV.IsMine)
        {
            Look();
            Move();
        }
    }


}
