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
    PhotonView PV;
    //public GameObject[] gunArray;
    //private Shoot shoot;

    //public Image boostBar;

    public Vector3 movement;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Start()
    {
        movementSpeed = startSpeed;
    }


    IEnumerator boostRecharge()
    {
        subtractSecond = true;

        yield return new WaitForSeconds(2);

        boostable = true;
        subtractSecond = false;
        //boostBar.fillAmount = 1;
    }

    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

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
    //}

}
