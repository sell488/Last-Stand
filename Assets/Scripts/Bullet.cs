using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //The bullet object
    public GameObject bullet;
    
    //The parent of the bullet. The object that will fire the bullet
    public Transform gun;


    // Start is called before the first frame update
    void Start()
    {
        FireBullet(10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireBullet(10);
        }
    }

    public void FireBullet(float velocity)
    {
        GameObject newBullet = Instantiate(bullet, gun.position, transform.rotation);

        newBullet.GetComponent<Rigidbody>().velocity = velocity * transform.up;
    }
}
