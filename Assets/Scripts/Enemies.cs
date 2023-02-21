using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Enemies : MonoBehaviour
{
    public GameObject spawner;
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

    // For enemies that shoot
    public Rigidbody bulletPrefab;
    public float shootSpeed = 300;

    private bool playerInRange = false;
    private float lastAttackTime = 0f;
    private float fireRate = 0.5f;

    void shootBullet()
    {
        var projectile = Instantiate(bulletPrefab, transform.position, transform.rotation);
        projectile.velocity = transform.forward * shootSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
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
        if(agent.remainingDistance < breakingDistance)
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

        Collider[] colliders2 = Physics.OverlapSphere(transform.position, 4);
        foreach(Collider c in colliders2)
        {
            if (c.gameObject.tag == "Player")
            {
                print("player in range!");
                if (Time.time - lastAttackTime >= 1f/fireRate)
                {
                    shootBullet();
                    lastAttackTime = Time.time;
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

}
