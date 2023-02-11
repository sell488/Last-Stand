using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPurchase : MonoBehaviour
{

    private ScoreKeeper sk;
    public GameObject player;
    

    [SerializeField]
    private TMP_Text score;

    public void hpTransaction()
    {
        sk = FindObjectOfType<ScoreKeeper>();
        PlayerHealth PH = player.GetComponent<PlayerHealth>();
        print(sk.killCount);
        print(PH.remainingHealth);

        if (sk.killCount >= 10 && PH.remainingHealth < 100)
        {

            sk.killCount -= 10;

            PH.takeDamage(-5);

            sk.setScoreDisplay("Kills: "+sk.killCount);
        }
           
    }
}
