using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float attackRadius;
    public float damage;
    public float attackCooldown;
    public GameObject bullet;
    public GameObject shootPoint;

    //private Collider[] colliders;

    public TrailRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        damager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void damager()
    {
        Collider[] colliders = new Collider[2];
        Physics.OverlapSphereNonAlloc(transform.position, attackRadius, colliders, LayerMask.GetMask("Enemy"));
        if (colliders[0])
        {
            colliders[0].gameObject.GetComponent<Enemies>().takeDamage(damage);
            shootPoint.transform.LookAt(colliders[0].gameObject.transform);
            GameObject newBullet = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * 100, ForceMode.VelocityChange);
        }
        Invoke("damager", attackCooldown);
    }
}
