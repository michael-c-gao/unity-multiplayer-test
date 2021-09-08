using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public GameObject[] weapons;

    public int minIndex = 0;
    public int maxIndex;
    public static int currWeapon;
    PhotonView PV;


    void Start()
    {
        currWeapon = 0;
        weapons[currWeapon].SetActive(true);
        maxIndex = weapons.Length - 1;
    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void setWeapon(int index, int one, int setIndex)
    {

        weapons[currWeapon].SetActive(false);
        if (currWeapon != index)
        {
            currWeapon += one;
        }
        else
        {
            currWeapon = setIndex;
        }
        weapons[currWeapon].SetActive(true);
    }

    void weaponSwap(int swap)
    {
        if (swap != currWeapon)
        {
            weapons[currWeapon].SetActive(false);
            currWeapon = swap;
            weapons[currWeapon].SetActive(true);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            setWeapon(maxIndex, 1, 0);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            setWeapon(minIndex, -1, maxIndex);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponSwap(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponSwap(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            weaponSwap(2);
        }
    }

}
