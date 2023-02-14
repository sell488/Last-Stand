using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Firearm
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Shoot()
    {
        GameObject bull = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Bullet bullScript = bull.GetComponent<Bullet>();



        if (bullScript)
        {
            bullScript.Initialize(shootPoint, muzzleVelocity, gravity);
        }
        if (primaryAmmo)
        {
            magRounds--;
        }
        if (OnShoot != null)
        {
            OnShoot();
        }

        Destroy(bull, 5f);
    }*/
}
