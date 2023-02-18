using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerPurchase : MonoBehaviour
{
    public ScoreKeeper sk;
    public GameObject towerPrefab;
    private TMP_Text scoreDisplay;


    public void towerTransaction()
    {
        sk = FindObjectOfType<ScoreKeeper>();
        scoreDisplay = sk.GetComponent<TMP_Text>();
        if (sk.killCount >= 1)
        {
            print("tower purchased");
            sk.killCount -= 1;
            scoreDisplay.text = "Kills: " + sk.killCount;
            Instantiate(towerPrefab, new Vector3(226, 6, 105), Quaternion.identity);
        }
    }

}