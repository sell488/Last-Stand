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

    /// <summary>
    /// How frequent should an enemy damage a player
    /// </summary>
    public float damage_CD;
    public float last_damaged;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        damage_CD = .8f;
        last_damaged = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (health <= 0 && collision.gameObject.GetComponent<Bullet>())
        {
            /*TODO
            Figure out multiple calls to Scorekeeper 
            */
            gameObject.SetActive(false);

            ScoreKeeper.ScorePoints(1);

            Object.Destroy(gameObject);
        } else if(collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
        //Invoke("checkHealth", 0.2f);
    }

    private void checkHealth()
    {
        if(health <= 0) { 
            ScoreKeeper.ScorePoints(1);
            print("destroyed");
            gameObject.SetActive(false);
            Object.Destroy(gameObject);
        }

        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        checkHealth();
        print("damaged");
    }
}
