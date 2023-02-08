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

    public float jump;

    private Rigidbody rb;

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

        rb.velocity = currentSpeed * movementDirection;       

        if(Input.GetKey(KeyCode.Space) && transform.position.y <= init_y)
        {
            rb.AddForce(new Vector3(0, jump, 0));
        }
    }

}
