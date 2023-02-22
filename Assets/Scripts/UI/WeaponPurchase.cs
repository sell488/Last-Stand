using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPurchase : MonoBehaviour
{

    public Firearm[] firearms;

    public void transactionWeapon1()
    {
        if (!firearms[0].isBought)
        {
            if(ScoreKeeper.getScore() >= 20)
            {
                ScoreKeeper.ScorePoints(-20);
                firearms[0].isBought = true;
                firearms[0].gameObject.SetActive(true);

                //Changing UI Text
                GetComponentInChildren<TMP_Text>().text = "Sold";

                //Changing UI Colors
                GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void transactionWeapon2()
    {
        if (!firearms[1].isBought)
        {
            if (ScoreKeeper.getScore() >= 25)
            {
                ScoreKeeper.ScorePoints(-25);
                firearms[1].isBought = true;
                firearms[1].gameObject.SetActive(true);

                //Changing UI Text
                GetComponentInChildren<TMP_Text>().text = "Sold";

                //Changing UI Colors
                GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void transactionWeapon3(){
        if (!firearms[2].isBought)
        {
            if (ScoreKeeper.getScore() >= 100)
            {
                ScoreKeeper.ScorePoints(-100);
                firearms[0].isBought = true;
                firearms[0].gameObject.SetActive(true);

                //Changing UI Text
                GetComponentInChildren<TMP_Text>().text = "Sold";

                //Changing UI Colors
                GetComponent<Image>().color = Color.red;
            }
        }

    }

    public bool buyWeapon(int weaponIndex, int price)
    {
        if (!firearms[weaponIndex].isBought)
        {
            if(ScoreKeeper.getScore() >= price)
            {
                ScoreKeeper.ScorePoints(-price);
                firearms[weaponIndex].isBought = true;
                return true;
            } else
            {
                return false;
            }
        }
        return false;
    }

}
