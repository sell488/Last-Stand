using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float amount;
    public float smoothing;
    private Vector3 initalPos;
    private Firearm firearm;

    private Vector3 velocity = Vector3.zero;

    public Animator ADS;

    void Start()
    {
        initalPos = transform.localPosition;
        firearm = GetComponentInChildren<Firearm>();
        ADS.SetBool("ADS", false);
    }

    private void Awake()
    {
        //ADS = GetComponent<Animator>();
    }
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        Vector3 nextPos = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            
            onUnaim();
            //ADS.Play("ADS");
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            onUnaim();
        }


    }

    private void onAim()
    {
        transform.position = Vector3.SmoothDamp(firearm.weaponPosition, firearm.WeaponADSPosition.position, ref velocity, firearm.sightAdjustmentSpeed * Time.deltaTime);
        //transform.position = firearm.weaponPosition;
    }

    private void onUnaim()
    {
        firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponDefaultPosition.position, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.position = firearm.weaponPosition;
    }
}
