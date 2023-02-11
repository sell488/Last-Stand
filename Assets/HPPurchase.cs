using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPurchase : MonoBehaviour
{
    public GameObject player;
    private ScoreKeeper sk;


    public void hpTransaction()
    {

        sk = FindObjectOfType<ScoreKeeper>();
        PlayerHealth PH = player.GetComponent<PlayerHealth>();
        if(sk.killCount >= 10 && PH.remainingHealth < 100)
        {
            sk.killCount -= 10;
            sk.setScoreDisplay("Kills :" + sk.killCount);

            PH.takeDamage(-5);
        }
        
    }
}
