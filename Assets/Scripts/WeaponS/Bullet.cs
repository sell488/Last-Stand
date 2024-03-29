using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float gravity;
    private float velocity;
    private Vector3 startPos;
    private Vector3 startFor;

    private bool isInit = false;
    private float startTime = -1;

    public float mass;
    /// <summary>
    /// Should the bullet explode on impact?
    /// </summary>
    public bool isExplosive;

    public TMP_Text Velocity;

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

    public float Cd;

    /// <summary>
    /// Modifier for damage
    /// </summary>
    public float fudgeFactor;

    private Quaternion initialRotation;

    public GameObject particleDustHit;

    private bool hitEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(Transform startPos, float velocity, float gravity)
    {
        this.startPos = startPos.position;
        this.startFor = startPos.forward.normalized;
        this.velocity = velocity;
        this.gravity = gravity;
        isInit = true;
        initialRotation = startPos.rotation;
        
    }

    private Vector3 calculateMotion(float time)
    {
        Vector3 point = startPos + (startFor * velocity * time);
        Vector3 gravityVec = 0.5f * Vector3.down * gravity * time * time;
        Vector3 nextPointRaw = point + gravityVec;
        return point + gravityVec;
    }

    private bool checkCollisionStep(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude, ~LayerMask.GetMask("Projectile", "Minimap"));
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInit || startTime < 0)
        {
            return;
        }

        float currentTime = Time.time - startTime;
        float nextTime = currentTime + Time.fixedDeltaTime;
        Vector3 currentPoint = calculateMotion(currentTime);
        Vector3 nextPoint = calculateMotion(nextTime);

        Vector3 distance = nextPoint - currentPoint;
        currentPoint = currentPoint - calculateDrag(distance);
        transform.position = currentPoint;
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

        Vector3 distance = nextPoint - currentPoint;

        currentPoint = currentPoint - calculateDrag(distance);

        if (prevTime > 0)
        {
            Vector3 prevPoint = calculateMotion(prevTime);
            if(checkCollisionStep(prevPoint, nextPoint, out hit))
            {
                OnHit(hit, currentPoint);
            }
        } else
        {
            if(checkCollisionStep(currentPoint, nextPoint, out hit))
            {
                OnHit(hit, currentPoint);
            }
        }

        

    }

    private void OnHit(RaycastHit hit, Vector3 currentPoint)
    {
        GameObject collision = hit.collider.gameObject;
        


        if(collision.GetComponent<Bullet>() == null)
        {
            if (collision.GetComponent<Enemies>() && !collision.GetComponent<Enemies>().isKilled)
            {
                collision.GetComponent<Enemies>().takeDamage(calculateDamage(currentPoint.magnitude));

                hitEnemy = true;

                if (!collision.GetComponent<Enemies>().isKilled)
                {
                    Destroy(gameObject, 5f);
                }
            } else if(collision.GetComponent<TerrainCollider>() && !hitEnemy)
            {
                GameObject particles = Instantiate(particleDustHit, hit.point + (hit.normal * 0.05f), Quaternion.LookRotation(hit.normal), transform.root.parent);
                ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
                Destroy(particles, 2f);
            } else if(collision.GetComponent<spawner>() && collision.GetComponent<spawner>().shieldDown)
            {
                collision.GetComponent<spawner>().takeDamage(calculateDamage(currentPoint.magnitude));
                Destroy(gameObject, 5f);
            }
            
            else
            {
                Destroy(gameObject, 5f);
            }
            
        }
        
        
    }

    private float calculateDamage(float vel)
    {
        return 100 * fudgeFactor * vel * mass;
    }


    /// <summary>
    /// Calculates the drag acting on the bullet 
    /// </summary>
    /// <returns>a float representing the force acting against the bullet</returns>
    private Vector3 calculateDrag(Vector3 currentVec)
    {

        //Velocity of the bullet decomposed
        float pointX = currentVec.x;
        float pointY = currentVec.y;
        float pointZ = currentVec.z;

        //Unit vectors of velocity
        float unitX = pointX / currentVec.magnitude;
        float unitY = pointY / currentVec.magnitude;
        float unitZ = pointZ / currentVec.magnitude;

        //Calculation of drag force for each component
        float dragX = 0.5f * pa * pointX * pointX * Cd * A * unitX;
        float dragY = 0.5f * pa * pointY * pointY * Cd * A * unitY;
        float dragZ = 0.5f * pa * pointZ * pointZ * Cd * A * unitZ;

        //dragY will always be negative because it is constantly being accelerated down
        Vector3 dragVec = new Vector3(dragX, -dragY, dragZ);

        return dragVec;
    }

    
}
