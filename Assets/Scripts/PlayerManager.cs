using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    //public GameObject[] characterLoad;
    public static int character;
    string a;
    Vector3 b;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        b = new Vector3(1, 1, 1);
        character = PlayerPrefs.GetInt("character");

        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        a = character.ToString();
        Debug.Log("Instantiated Player Controller");
        //PhotonNetwork.Instantiate("PlayerControllerz", Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(a, Vector3.zero, Quaternion.identity);
    }
}
