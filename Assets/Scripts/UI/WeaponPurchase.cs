using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPurchase : MonoBehaviour
{
    /// <summary>
    /// Weapon state variables
    /// </summary>
    public bool weapon1;
    public bool weapon2;
    public bool weapon3;

    /// <summary>
    /// button state variables
    /// </summary>

    

    public ScoreKeeper sk;

    private void Start()
    {
        weapon1 = false;
        weapon2 = false;
        weapon3 = false;
        sk = FindObjectOfType<ScoreKeeper>();
    }


    public void transactionWeapon1()
    {
        if (!weapon1)
        {
            if(sk.killCount >= 20)
            {
                sk.killCount -= 20;
                sk.setScoreDisplay("Kills: " + sk.killCount);
                weapon1 = true;

                //Changing UI Text
                GetComponentInChildren<TMP_Text>().text = "Sold";

                //Changing UI Colors
                GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void transactionWeapon2()
    {
        if (!weapon2)
        {
            if (sk.killCount >= 25)
            {
                sk.killCount -= 25;
                sk.setScoreDisplay("Kills: " + sk.killCount);
                weapon2 = true;

                //Changing UI Text
                GetComponentInChildren<TMP_Text>().text = "Sold";

                //Changing UI Colors
                GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void transactionWeapon3(){
        if (!weapon3)
        {
            if (sk.killCount >= 100)
            {
                sk.killCount -= 100;
                sk.setScoreDisplay("Kills: " + sk.killCount);
                weapon3 = true;

                //Changing UI Text
                GetComponentInChildren<TMP_Text>().text = "Sold";

                //Changing UI Colors
                GetComponent<Image>().color = Color.red;
            }
        }

    }

}
