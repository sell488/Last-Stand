using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Enemies : MonoBehaviour
{
    public GameObject spawner;

    /// <summary>
    /// minimap stuff
    /// </summary>
    public GameObject minimap_layer;
    public GameObject sphere;
    private float radius;


    private UnityEngine.AI.NavMeshAgent agent;

    /// <summary>
    /// What enemies should move towards
    /// </summary>
    private GameObject target;

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


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        //sphere = Instantiate(minimap_layer, transform.position, Quaternion.identity);


        // Minimap Stuff
        radius = 10.2f; 
    
        sphere = Instantiate(minimap_layer, transform.position , Quaternion.identity);
        squareConstraint(sphere.transform, target.transform, radius);
            

        //anim.GetComponent<Animator>();
        print(anim);
        accelerationSpeed = agent.acceleration;
        isKilled = false;
        damage_CD = .8f;
        last_damaged = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //update minimap spheres layer
        squareConstraint(sphere.transform, target.transform, radius);


        if (agent.remainingDistance < breakingDistance)
        {
            agent.acceleration = breakingSpeed;
        } else
        {
            agent.acceleration = accelerationSpeed;
        }
        agent.SetDestination(target.transform.position);
        //print(agent.velocity.magnitude);
        anim.SetFloat("Blend", agent.velocity.magnitude);
        agent.destination = target.transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, 2);
        foreach(Collider c in colliders)
        {
            if(c.gameObject.tag == "Player" && (Time.time - last_damaged > damage_CD))
            {
                last_damaged = Time.time;
                target.GetComponent<PlayerHealth>().takeDamage(damage);
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
        anim.Play("Take Damage");
        health -= damage;
        checkHealth();
        print("damaged");
    }

    private void squareConstraint(Transform spherePos, Transform targetPos, float radius)
    {
        Vector3 pos = new Vector3();

        pos.y = transform.position.y;

        if(Mathf.Abs(transform.position.x - targetPos.position.x) > radius)
        {
            pos.x = targetPos.position.x + Mathf.Sign(transform.position.x - targetPos.position.x) * radius;
        }
        else
        {
            pos.x = transform.position.x;
        }
        
        if (Mathf.Abs(transform.position.z - targetPos.position.z) > radius)
        {
            pos.z = targetPos.position.z + Mathf.Sign(transform.position.z - targetPos.position.z)*radius;
        }
        else
        {
            pos.z = transform.position.z;
        }

        spherePos.position = pos;

    }

}
