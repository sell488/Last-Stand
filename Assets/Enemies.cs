using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemies : MonoBehaviour
{
    public GameObject spawner;
    private float cur_time;
    private float x_speed;
    private float z_speed;
    private Vector3 direction;
    private Vector3 spawner_position;
    public UnityEngine.AI.NavMeshAgent agent;
     

    // Start is called before the first frame update
    void Start()
    {
        spawner_position = GameObject.FindGameObjectWithTag("spawner").transform.position;
        cur_time = Time.time;
        Random rnd = new Random();
        direction = transform.position - spawner_position;
        x_speed = .02f;
        z_speed = .02f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x + direction.x * x_speed
        //    , transform.position.y
        //    , transform.position.z + direction.z * z_speed);


        //// constraint
        //if(transform.position.y < spawner_position.y)
        //{
        //    transform.position = new Vector3(transform.position.x, spawner_position.y, transform.position.z);
        //}

        agent.SetDestination(new Vector3(30f, -3.2f, 16.5f));
    }
}
