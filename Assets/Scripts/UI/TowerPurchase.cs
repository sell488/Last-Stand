using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerPurchase : MonoBehaviour
{
    public GameObject towerPrefab;

    public GameObject[] towers;

    private int towerIndex = 0;

    public void towerTransaction()
    {

        if (ScoreKeeper.getScore() >= 3 && towerIndex < towers.Length)
        {
            print("tower purchased");
            ScoreKeeper.ScorePoints(-3);
            towers[towerIndex].SetActive(true);
            towerIndex++;
        }
    }

}