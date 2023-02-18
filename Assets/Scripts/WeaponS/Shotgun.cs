using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Firearm
{

    public int gauge;
    public float spread;



    public override void Shoot(GameObject proj)
    {
        float totalSpread = spread/gauge;
        for (int i = 0; i < gauge; i++)
        {
            Quaternion orgPos = shootPoint.rotation;
            Vector3 randomRotation = shootPoint.rotation.eulerAngles;
            randomRotation.x += Random.Range(-spread, spread);
            randomRotation.y += Random.Range(-spread, spread);
            Quaternion rotation = Quaternion.Euler(randomRotation);
            shootPoint.rotation = rotation;

            GameObject bull = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            Bullet bullScript = bull.GetComponent<Bullet>();

            if (bullScript)
            {
                bullScript.Initialize(shootPoint, muzzleVelocity, gravity);
            }

            shootPoint.rotation = orgPos;


            
            Destroy(bull, 5f);
        }

        if (primaryAmmo)
        {
            magRounds--;
        }
        fireEffect.Play(true);
        anim.Play("Fire");
        triggerOnShoot();

    }

    public override void Reload()
    {
        StartCoroutine("reloadCoroutine");
        
    }

    private IEnumerator reloadCoroutine()
    {
        
        while (magRounds < magCount && totalRounds > 0)
        {
            anim.Play("Reload");
            magRounds++;
            int rounds = magRounds;
            remainingRounds--;
            yield return new WaitForSeconds(0.5f);
            if(rounds != magRounds)
            {
                StopCoroutine("reloadCoroutine");
                magRounds--;
                
            }
        }
    }


}
