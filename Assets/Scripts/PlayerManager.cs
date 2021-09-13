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
    Quaternion rotation;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        //b = new Vector3(1, 1, 1);
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
        if(PhotonNetwork.IsMasterClient){
            b = new Vector3(-2, -2, -2);
            rotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            b = new Vector3(5, 5, 5);
            rotation = Quaternion.Euler(0, -180, 0);
        }
        PhotonNetwork.Instantiate(a, b, rotation);
    }
}
