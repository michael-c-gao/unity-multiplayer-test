using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks, IDamagable
{
    public CharacterController CC;
    public float movementSpeed;
    public float startSpeed;
    private float rotationSensitivity;
    public Transform playerRotate;
    float XRotation = 0f;
    PhotonView PV;
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    private Vector3 moveVector;
    public GameOver GameOver;
    public GameOver GameWin;
    public float dashSpeed;
    public float dashTime;
    public float cooldown;
    private float lastDashed = -9999f;
    public Image hb;
    public GameObject ui;

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
            Destroy(ui);
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

        moveVector = (transform.right * horizontal) + (transform.forward * vertical) + (transform.up * threed);

        CC.Move(moveVector * movementSpeed * Time.deltaTime);
    }


    IEnumerator Dash()
    {
        float starT = Time.time;
        while (Time.time < starT + dashTime)
        {
            CC.Move(moveVector * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;
        //else if (PV.IsMine)
        //{
            Look();
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > lastDashed + cooldown)
            {
                StartCoroutine(Dash());
                lastDashed = Time.time;
            }
        }
        //}
    }
    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage)
    {


        if (PV.IsMine)
        {
            currentHealth -= damage;
            hb.fillAmount = currentHealth / maxHealth;
        }
        
        if(currentHealth <= 0)
        {
            GameOver.Setup();
        }
    }

    /*
    [PunRPC]
    void go(float currentHealth)
    {

    }*/

}
