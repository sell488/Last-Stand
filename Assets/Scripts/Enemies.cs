using System.Collections;
using System.Collections.Generic;
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

    public float health;

    public float damage;

    public float breakingDistance;
    public float breakingSpeed;
    private float accelerationSpeed;

    private bool isKilled;

    public Animator anim;
     

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        //anim.GetComponent<Animator>();
        print(anim);
        accelerationSpeed = agent.acceleration;
        isKilled = false;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerHealth>() && !isKilled)
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }

        //Invoke("checkHealth", 0.2f);
    }

    private void checkHealth()
    {
        if(health <= 0) {
            isKilled = true;

            ScoreKeeper.ScorePoints(1);
            anim.SetBool("Take Damage", false);
            anim.SetBool("Blend Tree", false);
            anim.Play("Death");
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
        anim.Play("Take Damage");
        health -= damage;
        checkHealth();
        print("damaged");
    }
}
