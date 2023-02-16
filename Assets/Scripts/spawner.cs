using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    private float prev_time;
    private Random rnd;

    public float waveTimer;

    public float enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        prev_time = Time.time;
        rnd = new Random();

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - prev_time > waveTimer)
        {
            prev_time = Time.time;
            for(int i = 0; i < (int)(rnd.NextDouble() * enemyCount); i++)
            {
                Instantiate(enemy, transform.position + new Vector3((float)(rnd.NextDouble() - .5f) * 2.0f, transform.position.y, (float)(rnd.NextDouble() - .5f) * 2), Quaternion.identity);
            }
        }
    }
}
