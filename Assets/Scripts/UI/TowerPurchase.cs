using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerPurchase : MonoBehaviour
{
    public ScoreKeeper sk;
    public GameObject towerPrefab;

    public GameObject[] towers;

    private int towerIndex = 0;

    public void towerTransaction()
    {

        if (ScoreKeeper.getScore() >= 1)
        {
            print("tower purchased");
            ScoreKeeper.ScorePoints(-1);
            towers[towerIndex].SetActive(true);
            towerIndex++;
        }
    }

}