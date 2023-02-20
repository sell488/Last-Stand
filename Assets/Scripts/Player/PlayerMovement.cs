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

    public GameObject shop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        init_y = transform.position.y;
    }

    private void Update()
    {
        
        float currentSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
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
            if (shop.activeSelf)
            {
                shop.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
            else
            {
                shop.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
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
