using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPurchase : MonoBehaviour
{
    private Firearm firearm;
    private TMP_Text scoreDisplay;
    public WeaponSwitcher weaponSwitcher;


    public void ammoTransaction()
    {
        firearm = weaponSwitcher.currentGun.GetComponentInChildren<Firearm>();
        if (ScoreKeeper.getScore() >= 5 && firearm.remainingRounds <= firearm.totalRounds)
        {
            ScoreKeeper.ScorePoints(-5);
            firearm.remainingRounds = Mathf.Min(firearm.totalRounds, firearm.remainingRounds + 30);
        }
    }

}