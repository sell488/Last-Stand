using System.Runtime.CompilerServices;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    /// <summary>
    /// Rate at which bullets are fired, per minute
    /// </summary>
    public float fireRate;

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
    /// Total amount of rounds that may be fired before a reload is required 
    /// for <strong>secondary ammo</strong>
    /// </summary>
    public int magCountSec;

    /// <summary>
    /// Amount of time in seconds a reload should last when there is
    /// no bullet in the chamber (the player reloaded once magRoundsPrimary hit 0)
    /// </summary>
    /// <remarks>
    /// This is to represent the additional time needed to pull the charging handle 
    /// to chamber a round
    /// </remarks>
    public float reloadTime;
    /// <summary>
    /// Reload time for <strong>secondary ammo</strong>
    /// </summary>
    public float reloadTimeSec;

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
    /// The muzzle velocity of rounds fired from the weapon
    /// for <strong>secondary ammo</strong>
    /// </summary>
    public float muzzleVelocitySec;

    /// <summary>
    /// The total amount of individual rounds that can be carried by the player
    /// </summary>
    public int totalRounds;
    /// <summary>
    /// The total amount of individual rounds that can be carried by the player for
    /// <strong>secondary ammo</strong>
    /// </summary>
    public int totalRoundsSec;

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
    /// Internal tracker for <strong>secondary ammo</strong> rounds
    /// in mag left
    /// </summary>
    private int magRoundsSec;

    /// <summary>
    /// Internal tracker for total remaining rounds
    /// </summary>
    private int remainingRounds;
    /// <summary>
    /// Internal tracker for total remaining <strong>secondary ammo</strong>
    /// rounds
    /// </summary>
    private int remainingRoundsSec;

    /// <summary>
    /// Utility bool for preventing player from shooting while reloading
    /// </summary>
    private bool canFire;

    /// <summary>
    /// Internal bool tracker of which ammo type is being used. <strong>True</strong>: primary 
    /// ammo being used
    /// <strong>False</strong>: secondary ammo being used.
    /// </summary>
    /// <remarks>
    /// Defaults to True.
    /// </remarks>
    private bool primaryAmmo;

    /// <summary>
    /// Represents if a weapon is an automatic weapon
    /// </summary>
    public bool isAutomatic;

    /// <summary>
    /// keeps track of the current fire mode
    /// <strong>True</strong>: automatic mode
    /// <strong>False</strong>: semi mode
    /// </summary>
    /// <remarks>
    /// Defaults to false
    /// </remarks>
    private bool firemode;



    /// <summary>
    /// See <see cref="Bullet"/> for explanation of
    /// Vb, Db, and A
    /// </summary>
    /// <remarks>
    /// Used to assign the values neccesary for drag calculations
    /// to individual bullets fired by the gun
    /// </remarks>
    public float Vb;
    public float Db;
    public float A;


    // Start is called before the first frame update
    void Start()
    {
        remainingRounds = totalRounds;
        magRounds = magCount;
        canFire = true;
        fireRateSecs = 60f / fireRate;
        timeTillNextShot = Time.time + fireRateSecs;

        primaryAmmo = true;

        firemode = false;

        print(remainingRounds);
    }

    // Update is called once per frame
    void Update()
    {
        //Reload logic
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

        if(Input.GetKeyDown(KeyCode.V))
        {
            firemode = !firemode;
            print(firemode);
        }
        
    }

    private void FixedUpdate()
    {
        //Fire logic
        if(primaryAmmo == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && magRounds > 0 && firemode)
            {
                if (canFire)
                {
                    //Firerate logic.
                    if (Time.time >= timeTillNextShot)
                    {
                        FireBullet(bullet);
                        timeTillNextShot = Time.time + fireRateSecs;
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.Mouse0) && magRounds > 0 && !firemode)
            {
                if (Time.time >= timeTillNextShot)
                {
                    FireBullet(bullet);
                    timeTillNextShot = Time.time + fireRateSecs;
                }
            }
        } else if(primaryAmmo == false)
        {
            if (Input.GetKey(KeyCode.Mouse0) && magRoundsSec > 0)
            {
                if (canFire)
                {
                    //Firerate logic.
                    if (Time.time >= timeTillNextShot)
                    {
                        FireBullet(secondaryProjectile);
                        timeTillNextShot = Time.time + fireRateSecs;
                    }
                }
            }
        }
        
    }

    /// <summary>
    /// Fires a single bullet everytime FireBullet is called
    /// </summary>
    private void FireBullet(GameObject proj, float lifeTime = 5f)
    {
        Vector3 bulletPos = gameObject.GetComponent<Renderer>().bounds.center;
        bulletPos = bulletPos + gameObject.transform.forward;
        GameObject newBullet = Instantiate(proj, bulletPos, transform.rotation * Quaternion.Euler(90, 0, 0));

        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        bulletScript.Vb = this.Vb;
        bulletScript.Db = this.Db;
        bulletScript.A = this.A;

        newBullet.GetComponent<Rigidbody>().velocity = muzzleVelocity * transform.forward;

        magRounds--;
        print(magRounds);

        Object.Destroy(newBullet, lifeTime);
    }
    /// <summary>
    /// Reloads the gun. Should be called using a coroutine or Invoke
    /// </summary>
    private void Reload()
    {
        if (primaryAmmo == true)
        {
            remainingRounds = remainingRounds - (magCount - magRounds);

            ///If there are still rounds left in the mag, one will be left in the chamber
            ///This gives the player +1 round in their mag if they reload before
            ///completly emptying their gun
            if (magRounds > 0)
            {
                magRounds = magCount + 1;
                remainingRounds--;
            }
            else
            {
                magRounds = magCount;
            }

            print(remainingRounds);
        } else if(primaryAmmo== false)
        {
            remainingRoundsSec = remainingRoundsSec - (magCountSec - magRoundsSec);

            magRoundsSec = magCountSec;
        }
    }





}
