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

    private Vector3 weaponPos;

    private bool isAiming;

    void Start()
    {
        initalPos = transform.localPosition;
        firearm = GetComponentInChildren<Firearm>();
        ADS.SetBool("ADS", false);
        weaponPos = transform.localPosition;
        isAiming = false;
    }

    private void Awake()
    {
        //ADS = GetComponent<Animator>();
    }
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isAiming = true;
            onAim();
            //ADS.Play("ADS");
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            onUnaim();
        }

        if(!isAiming)
        {
            Vector3 nextPos = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        } else if(isAiming)
        {
            Vector3 nextPos = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        }


    }

    private void onAim()
    {
        //weaponPos = Vector3.Lerp(weaponPos, firearm.WeaponADSPosition.position, firearm.sightAdjustmentSpeed * Time.deltaTime);
        //transform.position = weaponPos;

        transform.localPosition = Vector3.Lerp(transform.localPosition, firearm.WeaponADSPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);

        //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, firearm.WeaponADSPosition.localPosition, ref velocity, firearm.sightAdjustmentSpeed * Time.deltaTime);
        //transform.position = firearm.weaponPosition;
    }

    private void onUnaim()
    {

        transform.localPosition = Vector3.Lerp(transform.localPosition, weaponPos, firearm.sightAdjustmentSpeed * Time.deltaTime);

        /*firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponDefaultPosition.position, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.localPosition = firearm.weaponPosition;*/
    }
}
