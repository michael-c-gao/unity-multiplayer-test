using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shoot : MonoBehaviourPunCallbacks
{
    public Camera cam;

    private float nextShot = 0f;

    public static float count = 1;
    //public float Damage;
    public float fireRate = 15f;
    public TrailRenderer tracerRound;
    public GameObject gunBarrel;

    public GameObject impact;
    bool activeWeapon = false;
    //public GameObject sniperScope;

    private static bool isADS = false;
    private static bool swappedADS = false;
    PhotonView PV;
    public float damage;
    PhotonView PV2;

    //[SerializeField] ParticleSystem attackParticle;
    void Awake()
    {
        PV = GetComponentInParent<PhotonView>();
        PV2 = GetComponent<PhotonView>();
    }

    void shootGun()
    {
        //RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(damage);
            PV2.RPC("RPC_Shoot", RpcTarget.All, hit.point); 

            //var tracer = Instantiate(tracerRound, gunBarrel.transform.position, Quaternion.identity);
            //tracer.AddPosition(gunBarrel.transform.position);
            /*
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.BulletHit(Damage);
                count += 5;
            }*/
            //tracer.transform.position = hit.point;
            //GameObject impactMark = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactMark, 3f);
        }
    }

    void weaponCheck(int currGun)
    {
        switch (currGun)
        {
            case 0:
                activeWeapon = Input.GetMouseButtonDown(0);
                swapADS();
                break;
            case 1:
                activeWeapon = Input.GetMouseButton(0);
                swapADS();
                break;
            case 2:
                activeWeapon = Input.GetMouseButtonDown(0);
                ADS();
                break;
        }
    }

    void ADS()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (!isADS)
            {
                cam.fieldOfView = 30.0f;
            }
            else
            {
                cam.fieldOfView = 60.0f;
            }
            //sniperScope.SetActive(!isADS);
            isADS = !isADS;
            swappedADS = !swappedADS;
        }
    }

    void swapADS()
    {
        if (swappedADS)
        {
            //bool check so camera is not continually set to 60 degrees over and over
            //sniperScope.SetActive(false);
            cam.fieldOfView = 60.0f;
            isADS = false;
            swappedADS = false;
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hit){
                   var tracer = Instantiate(tracerRound, gunBarrel.transform.position, Quaternion.identity);
                tracer.AddPosition(gunBarrel.transform.position);
            
            tracer.transform.position = hit;
            GameObject impactMark = Instantiate(impact, hit, Quaternion.LookRotation(hit));
            Destroy(tracer, 3f);
            Destroy(impactMark, 3f);

    }




    void Update()
    {

        if (!PV.IsMine)
            return;
        weaponCheck(Gun.currWeapon);
        if (activeWeapon && (Time.time >= nextShot))
        {
            nextShot = Time.time + 4 / fireRate;
            //attackParticle.Play();
            shootGun();
            if (isADS && Gun.currWeapon == 2)
            {
                //sniperScope.SetActive(false);
                cam.fieldOfView = 60.0f;
            }
        }
    }



}
