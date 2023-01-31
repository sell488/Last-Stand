using System.Runtime.CompilerServices;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    /// <summary>
    /// Rate at which bullets are fired, per minute
    /// </summary>
    public float fireRate;

    /// <summary>
    /// Bullet prefab to use
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// Prefab for a secondary projectile for weapons such as underbarrel grenade
    /// launchers or shotguns
    /// </summary>
    public GameObject secondaryProjectile;

    /// <summary>
    /// Total amount of rounds that may be fired before a reload is required
    /// </summary>
    public int magCount;

    /// <summary>
    /// Amount of time in seconds a reload should last when there is
    /// no bullet in the chamber (the player reloaded once magRounds hit 0)
    /// </summary>
    /// <remarks>
    /// This is to represent the additional time needed to pull the charging handle 
    /// to chamber a round
    /// </remarks>
    public float reloadTime;

    /// <summary>
    /// Amount of time in seconds a reload should last when there is still a bullet
    /// in the chamber.
    /// </summary>
    /// <remarks>
    /// This is to represent the time saved from not having to chamber a round
    /// </remarks>
    public float tacticalReloadTime;

    /// <summary>
    /// The muzzle velocity of rounds fired from the weapon
    /// </summary>
    public float muzzleVelocity;

    /// <summary>
    /// The total amount of individual rounds that can be carried by the player
    /// </summary>
    public int totalRounds;

    /// <summary>
    /// Internal variable for fire rate in seconds
    /// </summary>
    private float fireRateSecs;

    /// <summary>
    /// Time until a round can be shot again
    /// </summary>
    private float timeTillNextShot;

    /// <summary>
    /// Internal tracker for rounds in mag left
    /// </summary>
    private int magRounds;

    /// <summary>
    /// Internal tracker for total remaining rounds
    /// </summary>
    private int remainingRounds;

    /// <summary>
    /// Utility bool for preventing player from shooting while reloading
    /// </summary>
    private bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        remainingRounds = totalRounds;
        magRounds = magCount;
        canFire = true;
        fireRateSecs = 60f / fireRate;
        timeTillNextShot = Time.time + fireRateSecs;

        print(remainingRounds);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && magRounds > 0)
        {
            if (canFire)
            {

                if (Time.time >= timeTillNextShot)
                {
                    FireBullet();
                    timeTillNextShot = Time.time + fireRateSecs;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && remainingRounds > 0)
        {
            if(magRounds > 0)
            {
                Invoke("Reload", tacticalReloadTime);
            } else
            {
                Invoke("Reload", reloadTime);
            }
            

        }
        
    }

    /// <summary>
    /// Fires a single bullet everytime FireBullet is called
    /// </summary>
    private void FireBullet()
    {
        Vector3 bulletPos = gameObject.GetComponent<Renderer>().bounds.center;
        bulletPos = bulletPos + gameObject.transform.forward;
        GameObject newBullet = Instantiate(bullet, bulletPos, transform.rotation * Quaternion.Euler(90, 0, 0));

        newBullet.GetComponent<Rigidbody>().velocity = muzzleVelocity * transform.forward;

        magRounds--;
        print(magRounds);

        Object.Destroy(newBullet, 5.0f);
    }
    /// <summary>
    /// Reloads the gun. Should be called using a coroutine or Invoke
    /// </summary>
    private void Reload()
    {
        remainingRounds = remainingRounds - (magCount - magRounds);

        ///If there are still rounds left in the mag, one will be left in the chamber
        ///This gives the player +1 round in their mag if they reload before
        ///completly emptying their gun
        if(magRounds > 0)
        {
            magRounds = magCount + 1;
            remainingRounds--;
        } else
        {
            magRounds = magCount;
        }
        
        print(remainingRounds);
    }





}
