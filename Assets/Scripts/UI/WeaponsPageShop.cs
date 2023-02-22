using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPageShop : MonoBehaviour
{

    public GameObject HomeShop;


    public void toWeaponsPage()
    {
        if (HomeShop.activeSelf)
        {
            HomeShop.SetActive(false);
        }
    }

}
