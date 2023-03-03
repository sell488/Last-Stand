using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    private float prev_time;
    public bool spawningEnabled;
    /// <summary>
    /// The min number of enemies that can spawn at a spawner
    /// </summary>
    public int minEnemy;
    /// <summary>
    /// The max number of enemies that can spawn at a spawner
    /// </summary>
    public int maxEnemy;

    /// <summary>
    /// How often in seconds should a spawner spawn enemies when enabled;
    /// </summary>
    public float spawnFrequency = 3f;

    public Animator baseDestroyedAnim;

    [SerializeField]
    private Transform spawnPoint;

    [Range(1f, 20f)]
    public float difficultyCurveMin;
    [Range(1f, 20f)]
    public float difficultyCurveMax;

    public bool shieldDown = false;

    public float health = 100;

    private float initHealth;

    [SerializeField]
    private VisualEffect smokeStack;

    [SerializeField]
    private VisualEffect damageEffect;

    [SerializeField]
    private VisualEffect destructionEffect;

    // Start is called before the first frame update
    void Start()
    {
        prev_time = Time.time;
        spawningEnabled = false;
        initHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - prev_time > spawnFrequency)
        {
            prev_time = Time.time;
            if(spawningEnabled)
            {
                for (int i = 0; i < (int)(Random.Range(minEnemy, maxEnemy)); i++)
                {
                    Instantiate(enemy, spawnPoint.position + new Vector3((float)(Random.value - .5f) * 2.0f, transform.position.y, (float)(Random.value - .5f) * 2), Quaternion.identity);
                }
            }
        }
    }

    public void onNewWaveInit()
    {
        minEnemy = (int)(minEnemy * difficultyCurveMin);
        maxEnemy = (int)(maxEnemy * difficultyCurveMax);
        //(minEnemy (1000) * waveNumber)/50 * .10f
    }

    public void takeDamage(float damage)
    {
        if(health > 0 && health >= damage)
        {
            health -= damage;
        } else if(damage >= health)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            destructionEffect.transform.parent = null;
            destructionEffect.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 10, gameObject.transform.position.z);
            destructionEffect.gameObject.SetActive(true);
            Destroy(destructionEffect, 10f);
            baseDestroyedAnim.SetTrigger("Spawner Destroyed");
            damageEffect.gameObject.SetActive(false);
            ScoreKeeper.ScorePoints(5);
            ScoreKeeper.KilledSpawner();
            Destroy(gameObject, 10f);
            return;
        }

        if(health/initHealth < 0.5f)
        {
            smokeStack.gameObject.SetActive(false);
            damageEffect.gameObject.SetActive(true);
        }
    }

}
