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

    [SerializeField]
    private bool hasScope = false;

    private Crosshair _crosshair;



    void Start()
    {
        _crosshair = FindObjectOfType<Crosshair>();
        initalPos = transform.localPosition;
        firearm = GetComponentInChildren<Firearm>();
        //ADS.SetBool("ADS", false);
        weaponPos = transform.localPosition;
        isAiming = false;
        mouseLook = GetComponentInParent<MouseLook>();
        normalSensitivity = mouseLook.lookSensitivity;
        if(GetComponentInChildren<Camera>())
        {
            GetComponentInChildren<Camera>().enabled = false;
        }
        
    }

    private void Awake()
    {
        //ADS = GetComponent<Animator>();
    }
    void Update()
    {
        if(!mouseLook.startedGame)
        {
            return;
        }
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        Vector3 nextPos = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Slerp(transform.localPosition, nextPos + initalPos, Time.deltaTime * smoothing);

        if (Input.GetKey(KeyCode.Mouse1) && !GetComponentInParent<PlayerMovement>().isRunning)
        {
            isAiming = true;
            _crosshair.gameObject.SetActive(false);
            if (hasScope)
            {
                mouseLook.lookSensitivity = aimingSensitivity;
            }
            onAim();
            if (GetComponentInChildren<Camera>())
            {
                GetComponentInChildren<Camera>().enabled = true;
            }

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && !GetComponentInParent<PlayerMovement>().isRunning)
        {

            _crosshair.gameObject.SetActive(true);
            if (hasScope)
            {
                mouseLook.lookSensitivity = normalSensitivity;
            }

            isAiming = false;
            if (GetComponentInChildren<Camera>())
            {
                GetComponentInChildren<Camera>().enabled = false;
            }
            onUnaim();
        }
    }

    private void onAim()
    {
        firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponADSPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.localPosition = firearm.weaponPosition;
    }

    private void onUnaim()
    {
        firearm.weaponPosition = Vector3.Lerp(firearm.weaponPosition, firearm.WeaponDefaultPosition.localPosition, firearm.sightAdjustmentSpeed * Time.deltaTime);
        transform.localPosition = firearm.weaponPosition;
    }
}
