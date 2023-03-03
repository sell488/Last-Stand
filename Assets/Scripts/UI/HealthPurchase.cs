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
        
        PlayerHealth PH = player.GetComponent<PlayerHealth>();

        if (ScoreKeeper.getScore() >= 10 && PH.remainingHealth < 100)
        {

            ScoreKeeper.ScorePoints(-10);

            PH.takeDamage(-5);
        }
           
    }
}
