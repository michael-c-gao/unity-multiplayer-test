using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseMovement : MonoBehaviour
{
    private float rotationSensitivity;
    public Transform playerRotate;
    float XRotation = 0f;
    PhotonView PV;
    //public static bool isPaused = false;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Start()
    {
        rotationSensitivity = 125;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void FixedUpdate()
    {
        //if (!GameOver.isGameOver && !Pause.isPaused)
        //{
        //rotationSensitivity = SensSlider.sliderValue;

        if (!PV.IsMine)
            return;

        float Xaxis = Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        float Yaxis = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
        
        XRotation -= Yaxis;
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        playerRotate.Rotate(Vector3.up * Xaxis);
        //}
    }
}
