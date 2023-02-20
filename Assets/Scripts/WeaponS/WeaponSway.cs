using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float amount;
    public float smoothing;
    private Vector3 initalPos;
    private Firearm firearm;

    public float aimingSensitivity = 3f;

    private float normalSensitivity = 3f;

    private Vector3 velocity = Vector3.zero;

    public Animator ADS;

    private Vector3 weaponPos;

    private bool isAiming;

    private MouseLook mouseLook;



    void Start()
    {
        initalPos = transform.localPosition;
        firearm = GetComponentInChildren<Firearm>();
        //ADS.SetBool("ADS", false);
        weaponPos = transform.localPosition;
        isAiming = false;
        mouseLook = GetComponentInParent<MouseLook>();
        normalSensitivity = mouseLook.lookSensitivity;
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
            isAiming = true;

            mouseLook.lookSensitivity = aimingSensitivity;
            onAim();
            //ADS.Play("ADS");
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            mouseLook.lookSensitivity = normalSensitivity;
            isAiming = false;
            onUnaim();
        }

        /*if(!isAiming)
        {
            Vector3 nextPos = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        } else if(isAiming)
        {
            Vector3 nextPos = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        }*/


    }

    private void onAim()
    {
        //weaponPos = Vector3.Lerp(weaponPos, firearm.WeaponADSPosition.position, firearm.sightAdjustmentSpeed * Time.deltaTime);
        //transform.position = weaponPos;

        //transform.localPosition = Vector3.Lerp(transform.localPosition, firearm.WeaponADSPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);

        //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, firearm.WeaponADSPosition.localPosition, ref velocity, firearm.sightAdjustmentSpeed * Time.deltaTime);
        //transform.position = firearm.weaponPosition;

        firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponADSPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.localPosition = firearm.weaponPosition;
    }

    private void onUnaim()
    {

        //transform.localPosition = Vector3.Lerp(transform.localPosition, weaponPos, firearm.sightAdjustmentSpeed * Time.deltaTime);

        firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponDefaultPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.localPosition = firearm.weaponPosition;
    }
}
