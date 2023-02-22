using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class AmmoCount : MonoBehaviour
{

    public TMP_Text text;

    public Firearm firearm;

    public GameObject fireRate;

    // Start is called before the first frame update
    void Start()
    {
        firearm = FindObjectOfType<Firearm>();
        text = GetComponent<TMP_Text>();
        text.text = firearm.magRounds.ToString() + "/" + firearm.remainingRounds.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = firearm.magRounds.ToString() + "/" + firearm.remainingRounds.ToString();
    }

    public void setFireRate(bool isAutomatic)
    {
        fireRate.SetActive(isAutomatic);
    }
}
