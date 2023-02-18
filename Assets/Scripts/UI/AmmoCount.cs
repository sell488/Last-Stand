using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class AmmoCount : MonoBehaviour
{

    public TMP_Text text;

    public Firearm firearm;

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
}
