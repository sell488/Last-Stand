using System.Runtime.CompilerServices;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    /// <summary>
    /// Rate at which bullets are fired, per minute
    /// </summary>
    public float fireRate;

    public float gravity;

    [SerializeField]
    public Transform shootPoint;

    public GameObject bullet;

    public ParticleSystem fireEffect;
    public Animator anim;

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
    protected float fireRateSecs;

    /// <summary>
    /// Time until a round can be shot again
    /// </summary>
    protected float timeTillNextShot;

    /// <summary>
    /// Internal tracker for rounds in mag left
    /// </summary>
    public int magRounds;
    /// <summary>
    /// Internal tracker for <strong>secondary ammo</strong> rounds
    /// in mag left
    /// </summary>
    public int magRoundsSec;

    /// <summary>
    /// Internal tracker for total remaining rounds
    /// </summary>
    public int remainingRounds;
    /// <summary>
    /// Internal tracker for total remaining <strong>secondary ammo</strong>
    /// rounds
    /// </summary>
    public int remainingRoundsSec;

    /// <summary>
    /// Utility bool for preventing player from shooting while reloading
    /// </summary>
    public bool canFire;

    /// <summary>
    /// Internal bool tracker of which ammo type is being used. <strong>True</strong>: primary 
    /// ammo being used
    /// <strong>False</strong>: secondary ammo being used.
    /// </summary>
    /// <remarks>
    /// Defaults to True.
    /// </remarks>
    protected bool primaryAmmo;

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
    protected bool firemode;

    [SerializeField]
    private AmmoCount ammoUI;

    public bool isBought;

    /// <summary>
    /// Recoil Event Manager
    /// </summary>
    public delegate void RecoilEvent();
    public static event RecoilEvent OnShoot;

    /// <summary>
    /// Sight variables
    /// </summary>
    public Transform WeaponDefaultPosition;
    public Transform WeaponADSPosition;
    public Vector3 weaponPosition;

    [SerializeField]
    private Transform runningPosition;
    [SerializeField]
    public float sightAdjustmentSpeed;

    public bool isActive;

    private bool canReload;
    protected bool isReloading;

    private Camera playerCamera;

    [SerializeField]
    private GameObject crosshair;

    RaycastHit[] results = new RaycastHit[1];
    private Transform actualShootPoint;


    

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
        ammoUI = FindObjectOfType<AmmoCount>(); 
        ammoUI.setFireRate(firemode);
        weaponPosition = WeaponDefaultPosition.localPosition;
        canReload = true;
        isReloading = false;

        playerCamera = GetComponentInParent<Camera>();

        //shootPoint.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2f, Screen.height/2f, 10)));
    }

    public void OnEnable()
    {
        canFire = true;
        isReloading = false;
        GetComponentInParent<PlayerMovement>().canRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Reload logic
        if(Input.GetKeyDown(KeyCode.R) && remainingRounds > 0 && canReload)
        {
            GetComponentInParent<PlayerMovement>().canRun = false;
            isReloading = true;
            canFire = false;
            if(magRounds > 0)
            {
                anim.Play("Tactical Reload");
                Invoke("Reload", tacticalReloadTime);
            } else
            {
                anim.Play("Reload");
                Invoke("Reload", reloadTime);
            }
            

        }


        //Firemode logic
        if(Input.GetKeyDown(KeyCode.V) && isAutomatic)
        {
            firemode = !firemode;
            ammoUI.setFireRate(firemode);
        }

        //Fire logic
        if (primaryAmmo == true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && firemode)
            {
                if (canFire)
                {
                    //Firerate logic.
                    if (Time.time >= timeTillNextShot)
                    {
                        //FireBullet(bullet);
                        Shoot(bullet);
                        timeTillNextShot = Time.time + fireRateSecs;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !firemode)
            {

                if (canFire)
                {
                    if (Time.time >= timeTillNextShot)
                    {
                        Shoot(bullet);
                        timeTillNextShot = Time.time + fireRateSecs;
                    }
                }

            }
        }
        else if (primaryAmmo == false)
        {
            if (canFire && Time.time >= timeTillNextShot)
            {
                Shoot(secondaryProjectile);
                timeTillNextShot = Time.time + fireRateSecs;
            }
        }

        //RotateGun();
    }

    /*private void RotateGun()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, ~LayerMask.GetMask("Projectile")))
        {
            Vector3 direction = hitInfo.point - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }*/

    public void startRunning()
    {
        if(!isReloading)
        {
            canFire = false;
            canReload = false;
            anim.Play("Run");
            transform.rotation = runningPosition.rotation;
            transform.position = runningPosition.position;
        }
        
    }

    
    public void stopRunning()
    {
        anim.Play("Idle");
        canFire = true;
        canReload = true;
        transform.localRotation = WeaponDefaultPosition.localRotation;
        transform.position = WeaponDefaultPosition.position;
    }


    void FixedUpdate()
    {
        
    }

    protected bool getLookObject()
    {
        return false;
    }

    /// <summary>
    /// Reloads the gun. Should be called using a coroutine or Invoke
    /// </summary>
    public virtual void Reload()
    {
        if (primaryAmmo == true && canReload)
        {
            if(remainingRounds < magCount && remainingRounds > 0)
            {
                magRounds = remainingRounds;
                remainingRounds = 0;
            } else
            {
                remainingRounds = remainingRounds - (magCount - magRounds);
            }
            

            ///If there are still rounds left in the mag, one will be left in the chamber
            ///This gives the player +1 round in their mag if they reload before
            ///completly emptying their gun
            if (magRounds > 0 && remainingRounds != 0)
            {
                magRounds = magCount + 1;
                remainingRounds--;
            }
            else if(remainingRounds != 0)
            {
                magRounds = magCount;
            }

            if(magRounds > 0)
            {
                ReloadAlert.stopReloadAlert();
            }

            canFire = true;

        } else if(primaryAmmo== false)
        {
            remainingRoundsSec = remainingRoundsSec - (magCountSec - magRoundsSec);

            magRoundsSec = magCountSec;
            canFire = true;
        }
        isReloading = false;
        GetComponentInParent<PlayerMovement>().canRun = true;
    }

    public virtual void Shoot(GameObject proj)
    {
        
        if(!(magRounds > 0))
        {
            ReloadAlert.startReloadAlert();
            return;
        }

        /*RaycastHit hit;
        Vector3 rayPoint = (crosshair.transform.position + (crosshair.transform.TransformDirection(Vector3.forward) * 200));

        if (Physics.Raycast(crosshair.transform.position, crosshair.transform.TransformDirection(Vector3.forward), out hit))
        {
            actualShootPoint = shootPoint;
            actualShootPoint.LookAt(hit.point);
        }
        else
        {
            actualShootPoint = shootPoint;
            actualShootPoint.LookAt(rayPoint);
        }*/

        //Ray ray = new Ray(crosshair.transform.position, crosshair.transform.forward);
        //Physics.RaycastNonAlloc(ray, results);

        
        /*if (Physics.Raycast(crosshair.transform.position, crosshair.transform.forward, out hit))
        {
            print("raycast worked: " + hit.transform.gameObject.name);
            actualShootPoint = shootPoint;
            actualShootPoint.LookAt(hit.transform);
        } else
        {
            actualShootPoint = shootPoint;
        }*/
        GameObject bull = Instantiate(proj, shootPoint.position, shootPoint.rotation);
        Bullet bullScript = bull.GetComponent<Bullet>();

        

        if(proj.GetComponent<Rigidbody>())
        {
            bullScript = bull.GetComponent<RBBullet>();
            proj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, muzzleVelocitySec, 0));
        } 

        if(bullScript)
        {
            bullScript.Initialize(shootPoint, muzzleVelocity, gravity);
        }
        if (primaryAmmo)
        {
            magRounds--;
        }
        if(OnShoot != null)
        {
            OnShoot();
        }
        fireEffect.Play(true);
        if(((float)magRounds)/((float)magCount) < 0.1f)
        {
            ReloadAlert.startReloadAlert();
        }
        Destroy(bull, 5f);
    }

    public void triggerOnShoot()
    {
        if(OnShoot != null)
        {
            OnShoot();
        }
    }
}
