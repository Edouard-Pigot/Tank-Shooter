using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    public GameObject spawnPos;
    public GameObject bulletObj;

    bool canFire = true;

    void Update()
    {
        FireBullet();
    }

    void FireBullet()
    {
        if ((Input.GetMouseButtonDown(0)) && canFire)
        {
            GameObject newBullet = Instantiate(bulletObj, spawnPos.transform.position, spawnPos.transform.rotation) as GameObject;

            canFire = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canFire = true;
        }
    }
}
