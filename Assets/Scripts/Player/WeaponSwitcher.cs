using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    public GameObject[] guns;

    public GameObject currentGun;

    private AmmoCount ammoCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentGun = guns[0];
        currentGun.gameObject.SetActive(true);

        ammoCounter = FindObjectOfType<AmmoCount>();

        for (int i = 1; i < guns.Length; i++)
        {
            guns[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Keypad1))
        {
            switchWeapon(0);
        }
        if(Input.GetKey(KeyCode.Keypad2))
        {
            switchWeapon(1);
        }
    }

    void switchWeapon(int index)
    {
        //if (!currentGun == guns[index])
        //{
            currentGun.gameObject.SetActive(false);
            currentGun = guns[index];
            currentGun.gameObject.SetActive(true);
            ammoCounter.firearm = currentGun.GetComponent<Firearm>();
       // }
    }
}
