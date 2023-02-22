using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponButton : MonoBehaviour
{

    public int index;

    public int price;

    private WeaponPurchase shop;

    private void Start()
    {
        shop = GetComponentInParent<WeaponPurchase>();
    }

    public void tryPurchase()
    {
        if(shop.buyWeapon(index, price))
        {
            //Changing UI Text
            GetComponentInChildren<TMP_Text>().text = "Sold";

            //Changing UI Colors
            GetComponent<Image>().color = Color.red;
        }
    }
 
}
