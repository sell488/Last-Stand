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

    // A field editable from inside Unity with a default value of 5
    public float speed = 5.0f;
    public float sprintSpeed = 10.0f;

    // How much will the player slide on the ground
    // The lower the value, the greater distance the user will slide
    public float drag;

    public float jump;

    private Rigidbody rb;

    private Vector3 yLock;

    // Start is called before the first frame update
    void Start()
    {
        yLock = new Vector3(1, 0, 1);
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
        Vector3 total_v = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.W))
        {
            total_v += (transform.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            total_v += -transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            total_v += -transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            total_v += transform.right;
        }
        total_v.y = 0; // = Vector3.Scale(total_v, yLock);
        total_v.Normalize();
        rb.velocity = currentSpeed * total_v;

        if(Input.GetKey(KeyCode.Space) && transform.position.y <= init_y)
        {
            rb.AddForce(new Vector3(0, jump, 0));
        }
    }

}
