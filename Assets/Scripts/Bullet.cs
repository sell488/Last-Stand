using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float gravity;
    private float velocity;
    private Vector3 startPos;
    private Vector3 startFor;

    private bool isInit = false;
    private float startTime = -1;

    private float mass;
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

    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Density of the bullet
        /*pb = GetComponent<Rigidbody>().mass / Vb;

        rb = gameObject.GetComponent<Rigidbody>();*/
    }

    public void Initialize(Transform startPos, float velocity, float gravity, float mass)
    {
        this.startPos = startPos.position;
        this.startFor = startPos.forward.normalized;
        this.velocity = velocity;
        this.gravity = gravity;
        this.mass = mass;
        isInit = true;
        initialRotation = transform.rotation;
        
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
        
        bool test = Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
        RaycastHit testhit = hit;
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
        transform.position = currentPoint;
        //transform.rotation = Quaternion.LookRotation(currentPoint) * Quaternion.Euler(0, 90, 90);

        //transform.up = Vector3.Lerp(transform.up, currentPoint, Time.deltaTime);

        transform.rotation = Quaternion.LookRotation(currentPoint) * Quaternion.Euler(90, 90, 0);
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
        float prevTime = currentTime - Time.fixedDeltaTime;

        Vector3 currentPoint = calculateMotion(currentTime);
        Vector3 nextPoint = calculateMotion(nextTime);

        if (prevTime > 0)
        {
            Vector3 prevPoint = calculateMotion(prevTime);
            if(checkCollisionStep(prevPoint, nextPoint, out hit))
            {
                
                OnHit(hit, currentPoint);
            }
        }

        //transform.position = currentPoint;

        
    }

    private void OnHit(RaycastHit hit, Vector3 currentPoint)
    {
        GameObject collision = hit.collider.gameObject;
        

        print("hit");

        if (collision.GetComponent<Enemies>())
        {
            print("hit enemy");
            collision.GetComponent<Enemies>().takeDamage(currentPoint.magnitude);
            ScoreKeeper.ScorePoints(1);

            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    private float calculateDamage(float vel)
    {
        return fudgeFactor * vel * mass;
    }


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
