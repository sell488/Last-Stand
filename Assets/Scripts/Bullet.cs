using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float gravity;
    private float velocity;
    private Vector3 startPos;
    private Vector3 startFor;

    private bool isInit = false;
    private float startTime = -1;

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

    private Rigidbody rb;

    /// <summary>
    /// Modifier for damage
    /// </summary>
    public float fudgeFactor;

    // Start is called before the first frame update
    void Start()
    {
        //Density of the bullet
        /*pb = GetComponent<Rigidbody>().mass / Vb;

        rb = gameObject.GetComponent<Rigidbody>();*/
    }

    public void Initialize(Transform startPos, float velocity, float gravity)
    {
        this.startPos = startPos.position;
        this.startFor = startPos.forward.normalized;
        this.velocity = velocity;
        this.gravity = gravity;
        isInit = true;
        
    }

    private Vector3 calculateMotion(float time)
    {
        Vector3 point = startPos + (startFor * velocity * time);
        Vector3 gravityVec = Vector3.down * gravity * time * time;
        Vector3 nextPointRaw = point + gravityVec;
        return point + gravityVec;
    }

    private bool checkCollisionStep(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInit || startTime < 0)
        {
            return;
        }

        float currentTime = Time.time - startTime;
        Vector3 currentPoint = calculateMotion(currentTime);
        print(currentPoint);
        transform.position = currentPoint;
        transform.rotation = Quaternion.LookRotation(currentPoint) * Quaternion.Euler(0, 90, 90);
    }

    private void FixedUpdate()
    {
        if (!isInit) { return; }
        if (startTime < 0)
        {
            startTime = Time.time;
        }
        RaycastHit hit;
        float currentTime = Time.time - startTime;
        float nextTime = currentTime + Time.fixedDeltaTime;

        Vector3 currentPoint = calculateMotion(currentTime);
        Vector3 nextPoint = calculateMotion(nextTime);

        if(checkCollisionStep(currentPoint, nextPoint, out hit))
        {
            Destroy(gameObject);
        }

        /*if (rb.velocity.magnitude != 0)
        {
            Vector3 direction = -GetComponent<Rigidbody>().velocity.normalized;
            float drag = calculateDrag();

            GetComponent<Rigidbody>().AddForce(direction * calculateDrag());
            if (GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity) * Quaternion.Euler(0, 90, 90);
            }
        }*/
        
        

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemies>())
        {
            collision.gameObject.GetComponent<Enemies>().health = collision.gameObject.GetComponent<Enemies>().health -
                calculateDamage();
        } else if(collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(calculateDamage());
        }

        //Object.Destroy(gameObject);
    }*/

    /*private float calculateDamage()
    {
        return fudgeFactor * (rb.mass * rb.velocity.magnitude);
    }*/


    /// <summary>
    /// Calculates the drag acting on the bullet 
    /// </summary>
    /// <returns>a float representing the force acting against the bullet</returns>
    private float calculateDrag(Vector3 currentVec)
    {

        //Velocity of the bullet
        float v = currentVec.magnitude;


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
