using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Should the bullet explode on impact?
    /// </summary>
    public bool isExplosive;

    /// <summary>
    /// Volume of a bullet.
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    public float Vb;

    /// <summary>
    /// Diameter of the bullet.
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    public float Db;

    /// <summary>
    /// Cross sectional area of a bullet.
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    public float A;

    /// <summary>
    /// Density of air (constant).
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    private float pa = 1.225f;

    /// <summary>
    /// Density of the bullet
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    private float pb;

    // Start is called before the first frame update
    void Start()
    {
        //Density of the bullet
        pb = GetComponent<Rigidbody>().mass / Vb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 direction = -GetComponent<Rigidbody>().velocity.normalized;
        float drag = calculateDrag();

        GetComponent<Rigidbody>().AddForce(direction * calculateDrag());
        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity) * Quaternion.Euler(0, 90, 90);

    }

    /// <summary>
    /// Calculates the drag acting on the bullet 
    /// </summary>
    /// <returns>a float representing the force acting against the bullet</returns>
    private float calculateDrag()
    {
        //Rigidbody of the object
        Rigidbody rb = GetComponent<Rigidbody>();

       
        //Velocity of the bullet
        float v = rb.velocity.magnitude;


        ///<summary>
        /// Calculates the drag coefficient, Cd using the formula
        /// Cd = 8/(pb * v^2 * pi * d^2)
        /// </summary>
        float dragCof = 8/(pb*v*v*Mathf.PI*Db*Db);

        //Calculates drag
        float drag = 0.5f*pa*v*v*dragCof*A;

        return drag;
    }
}
