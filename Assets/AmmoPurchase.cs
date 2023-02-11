using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPurchase : MonoBehaviour
{
    public ScoreKeeper sk;
    private Firearm firearm;
    private TMP_Text scoreDisplay;


    public void transaction()
    {
        sk = FindObjectOfType<ScoreKeeper>();
        firearm = FindObjectOfType<Firearm>();
        scoreDisplay = sk.GetComponent<TMP_Text>();
        if (sk.killCount >= 5 && firearm.remainingRounds <= firearm.totalRounds)
        {
            sk.killCount -= 5;
            firearm.remainingRounds = Mathf.Min(firearm.totalRounds, firearm.remainingRounds + 30);
            scoreDisplay.text = "Kills: " + sk.killCount;
        }
    }
}
