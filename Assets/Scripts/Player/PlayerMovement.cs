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
    public GameObject shop;

    // A field editable from inside Unity with a default value of 5
    public float speed = 5.0f;

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

    // Update is called once per frame
    /*void Update()
    {
        // This will detect forward and backward movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // This will detect sideways movement
        verticalMovement = Input.GetAxisRaw("Vertical");

        

        // Calculate the direction to move the player
        Vector3 movementDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        
        // Move the player
        rb.AddForce(movementDirection * speed, ForceMode.Force);
        // Apply drag
        rb.drag = drag;
    }*/

    private void Update()
    {
  
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
        total_v = Vector3.Scale(total_v, yLock);
        total_v.Normalize();
        rb.velocity = speed * total_v;

        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jump, 0));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shop.activeSelf)
            {
                shop.SetActive(false);
                Cursor.visible = false;
            }
            else
            {
                shop.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

}
