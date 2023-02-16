using StarterAssets;
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

    Animator anim;

    private bool isWalking;


    // Start is called before the first frame update
    void Start()
    {


        anim = gameObject.GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        target = GameObject.FindGameObjectWithTag("Player");
        isWalking = true;
        anim.SetBool("Open_Anim", true);
        anim.SetBool("Walk_Anim", true);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
        /*if(agent.destination.magnitude < 5 && isWalking)
        {
            anim.SetBool("Walk_Anim", false);
            isWalking = false;
        }*/
        anim.SetFloat("Walk_Anim", agent.speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health <= 0 && collision.gameObject.GetComponent<Bullet>())
        {
            /*TODO
            Figure out multiple calls to Scorekeeper 
            */
            gameObject.SetActive(false);

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
