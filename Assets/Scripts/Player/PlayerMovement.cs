using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// https://kodacoding.com/player-movement-in-unity/
public class PlayerMovement : MonoBehaviour
{

    private float horizontalMovement;
    private float verticalMovement;
    private float init_y; 

    public float speed = 5.0f;
    public float sprintSpeed = 10.0f;

    public float jump = 5;

    private Rigidbody rb;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool IsGrounded;

    public GameObject homeShop;
    public GameObject weaponShop;

    [Header("Head bob parameters")]
    //Head bobbing vars
    [SerializeField]
    private bool enableHeadBob = true;

    [SerializeField, Range(0, 0.1f)]
    private float walkBobAmount = 0.05f;
    [SerializeField, Range(0, 30)]
    private float walkBobSpeed = 14f;

    [SerializeField, Range(0, 0.1f)]
    private float sprintBobAmount = 0.11f;
    [SerializeField, Range(0, 30)]
    private float sprintBobSpeed = 18f;

    [SerializeField]
    public Transform camera = null;
    [SerializeField]
    private Transform cameraHolder = null;

    private float defaultY = 0f;
    private float timer;

    public bool isRunning;
    private bool hasPositionedRunning;
    private bool hasPositionedWalking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        init_y = transform.position.y;
        defaultY = camera.transform.localPosition.y;
        hasPositionedRunning = false;
        hasPositionedWalking = true;
        isRunning = false;
    }

    private void Update()
    {
        
        float currentSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            isRunning = true;
            print("running");
            if(!hasPositionedRunning)
            {
                GetComponentInChildren<WeaponSwitcher>().currentGun.GetComponentInChildren<Firearm>().startRunning();
                hasPositionedRunning = true;
                hasPositionedWalking = false;
            }
        }
            
        else
        {
            currentSpeed = speed;
            isRunning = false;
            if(!hasPositionedWalking)
            {
                GetComponentInChildren<WeaponSwitcher>().currentGun.GetComponentInChildren<Firearm>().stopRunning();
                hasPositionedWalking = true;
                hasPositionedRunning = false;
            }
            
        }
        // This will detect forward and backward movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // This will detect sideways movement
        verticalMovement = Input.GetAxisRaw("Vertical");

        // Calculate the direction to move the player
        Vector3 movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        
        movementDirection.y = 0;
        movementDirection.Normalize();

        rb.velocity = currentSpeed * movementDirection + new Vector3 (0, rb.velocity.y, 0);       

        if(Input.GetKey(KeyCode.Space) && (IsGrounded || transform.position.y <= init_y))
        {
            rb.velocity = Vector3.up * jump;
        }

        if (rb.velocity.y < 0) //https://youtu.be/7KiK0Aqtmzc (better jump physics)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        if (rb.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (weaponShop.activeSelf)
            {
                homeShop.SetActive(false);
                weaponShop.SetActive(false);
                GetComponentInChildren<Firearm>().canFire = true;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
            else
            {
                homeShop.SetActive(true);
                weaponShop.SetActive(true);
                GetComponentInChildren<Firearm>().canFire = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
        }
        if (enableHeadBob)
        {
            handleHeadBob();
        }
        
    }

    private void handleHeadBob()
    { 
        if(true)
        {
            if(Mathf.Abs(rb.velocity.x) > 1 || Mathf.Abs(rb.velocity.z) > 1)
            {
                timer += Time.deltaTime * (isRunning ? sprintBobSpeed : walkBobSpeed);
                camera.transform.localPosition = new Vector3(
                    camera.transform.localPosition.x,
                    defaultY + Mathf.Sin(timer) * (isRunning ? sprintBobAmount : walkBobAmount),
                    camera.transform.localPosition.z
                    );
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Level")
        {
            IsGrounded = true;
            Debug.Log("Grounded");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Level")
        {
            IsGrounded = false;
            Debug.Log("Not Grounded");
        }
    }

}
