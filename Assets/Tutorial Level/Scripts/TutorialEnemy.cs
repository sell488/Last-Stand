using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;

public class TutorialEnemy : Enemies
{
    [SerializeField]
    private bool followPlayer;
    void Start()
    {
        cameraSize = 25;
        // Minimap Stuff
        radius = cameraSize;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();


        sphere = Instantiate(minimap_layer, transform.position, Quaternion.identity);
        sphereConstraint(sphere.transform, target.transform, radius);

        accelerationSpeed = agent.acceleration;
        defaultSpeed = 8;
        isKilled = false;
        //damage_CD = .8f;
        last_damaged = 0;
        player = FindObjectOfType<PlayerHealth>().gameObject;
        agent.SetDestination(target.transform.position);
        if(FindObjectOfType<Base>())
        {
            playerBase = FindObjectOfType<Base>().gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        //update minimap spheres layer
        if (!isKilled)
        {
            sphereConstraint(sphere.transform, player.transform, radius);



            if (agent.remainingDistance < breakingDistance)
            {
                agent.acceleration = breakingSpeed;
            }
            else
            {
                agent.acceleration = accelerationSpeed;
            }
        }
        sphereConstraint(sphere.transform, target.transform, radius);

        anim.SetFloat("Blend", agent.velocity.magnitude);
        

        if (agent.remainingDistance > aggroDistance && !isBeingDamaged && !isKilled)
        {
            agent.speed = distanceSpeed;
        }
        else if (!isBeingDamaged && !isKilled && agent.remainingDistance < aggroDistance)
        {
            agent.speed = defaultSpeed;
        }

        if(followPlayer)
        {
            agent.SetDestination(player.transform.position);
        }

        Collider[] colliders = new Collider[3];
        Physics.OverlapSphereNonAlloc(transform.position, attackRadius, colliders, (LayerMask.GetMask("Player") | LayerMask.GetMask("Base")));
        foreach (Collider c in colliders)
        {
            if (c)
            {
                if (c.GetComponent<Base>() && (Time.time - last_damaged > damage_CD) && !isKilled)
                {
                    last_damaged = Time.time;
                    anim.Play("Attack");
                    c.GetComponent<Base>().changeHealth(-damage);
                }
            }
        }
    }
}
