using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float amount;
    public float smoothing;
    private Vector3 initalPos;
    private Firearm firearm;

    void Start()
    {
        initalPos = transform.localPosition;
        firearm = GetComponentInChildren<Firearm>();
    }
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        Vector3 nextPos = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            onAim();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            onUnaim();
        }


    }

    private void onAim()
    {
        Vector3 weaponPos;

        weaponPos = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponADSPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.position = weaponPos;
    }

    private void onUnaim()
    {
        firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponDefaultPosition.position, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.position = firearm.weaponPosition;
    }
}
