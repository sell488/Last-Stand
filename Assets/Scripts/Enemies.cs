using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemies : MonoBehaviour
{
    public GameObject spawner;
    public UnityEngine.AI.NavMeshAgent agent;

    /// <summary>
    /// What enemies should move towards
    /// </summary>
    public GameObject target;

    public float health;
     

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health <= 0 && collision.gameObject.GetComponent<Bullet>())
        {
            ScoreKeeper.ScorePoints(1);

            Object.Destroy(gameObject);
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
}
