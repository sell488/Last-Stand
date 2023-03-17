using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageShop : MonoBehaviour
{
    public GameObject HomeShop;

    public void toHomePageShop()
    {
        if (!HomeShop.activeSelf)
        {
            HomeShop.SetActive(true);
        }
    }

}
