using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Enemies : MonoBehaviour
{
    public GameObject spawner;
    private UnityEngine.AI.NavMeshAgent agent;

    /// <summary>
    /// What enemies should move towards
    /// </summary>
    private GameObject target;

    private GameObject player;
    private GameObject playerBase;

    /// <summary>
    /// Enemy health
    /// </summary>
    public float health;

    /// <summary>
    /// Amount of damage an enemy does to the player
    /// </summary>
    public float damage;

    /// <summary>
    /// The distance from the target an enemy will switch to a faster decceleration
    /// </summary>
    public float breakingDistance;
    /// <summary>
    /// The rate at which the agent will slow down when within the breaking distance
    /// </summary>
    public float breakingSpeed;
    /// <summary>
    /// The original acceleration
    /// </summary>
    private float accelerationSpeed;

    /// <summary>
    /// tracks if the enemy should be dead
    /// </summary>
    public bool isKilled;

    /// <summary>
    /// animation controller
    /// </summary>
    public Animator anim;

    /// <summary>
    /// Particle effect that is played when the enemy takes damage
    /// </summary>
    public ParticleSystem hitEffect;

    private Color baseColor;
    public Color deathColor;
     
    /// How frequent should an enemy damage a player
    /// </summary>
    public float damage_CD;
    public float last_damaged;
    public float attackRadius;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        accelerationSpeed = agent.acceleration;
        isKilled = false;
        //damage_CD = .8f;
        last_damaged = 0;
        player = FindObjectOfType<PlayerHealth>().gameObject;
        playerBase = FindObjectOfType<Base>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance < breakingDistance)
        {
            agent.acceleration = breakingSpeed;
        } else
        {
            agent.acceleration = accelerationSpeed;
        }
        
        anim.SetFloat("Blend", agent.velocity.magnitude);
        //agent.destination = target.transform.position;
        NavMeshPath playerPath = new NavMeshPath();
        NavMeshPath basePath = new NavMeshPath();

        if (agent.CalculatePath(player.transform.position, playerPath) && agent.CalculatePath(playerBase.transform.position, basePath))
        {
            if(CalculatePathLength(player.transform.position) <= CalculatePathLength(playerBase.transform.position)) {
                agent.SetDestination(player.transform.position);
            } else
            {
                agent.SetDestination(playerBase.transform.position);
            }
        }

        Collider[] colliders = new Collider[3];
        Physics.OverlapSphereNonAlloc(transform.position, attackRadius, colliders, ~LayerMask.GetMask("Enemy"));
        foreach (Collider c in colliders)
        {
            if(c)
            {
                if ((c.gameObject.tag == "Player") && (Time.time - last_damaged > damage_CD) && !isKilled)
                {
                    last_damaged = Time.time;
                    c.GetComponent<PlayerHealth>().takeDamage(damage);
                    anim.Play("Attack");
                }
                else if (c.GetComponent<Base>() && (Time.time - last_damaged > damage_CD) && !isKilled && c.GetComponent<Base>().health != 0)
                {
                    last_damaged = Time.time;
                    anim.Play("Attack");
                    c.GetComponent<Base>().changeHealth(-damage);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerHealth>() && !isKilled)
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
        else if (collision.gameObject.tag == "Tower")
        {
            print("enemy hit tower");
            Destroy(gameObject);
        }
        //Invoke("checkHealth", 0.2f);
    }

    private void checkHealth()
    {
        if(health <= 0) {
            isKilled = true;

            ScoreKeeper.ScorePoints(1);
            anim.StopPlayback();
            anim.Play("Death");
            gameObject.GetComponent<MeshCollider>().enabled = false;
            agent.speed = 0;
            Invoke("destroy", 5);
            print("destroyed");
        }

        
    }

    private void destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void takeDamage(float damage)
    {
        hitEffect.Play(true);
        anim.StopPlayback();
        anim.Play("Take Damage");
        health -= damage;
        checkHealth();
        print("damaged");
    }

    /// <summary>
    /// https://www.reddit.com/r/Unity3D/comments/2zfaeu/grabbing_distances_on_navmesh/
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

}
