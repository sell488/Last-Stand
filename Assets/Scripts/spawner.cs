using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        prev_time = Time.time;
        spawningEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - prev_time > 3.0f)
        {
            prev_time = Time.time;
            if(spawningEnabled)
            {
                for (int i = 0; i < (int)(Random.Range(minEnemy, maxEnemy)); i++)
                {
                    Instantiate(enemy, transform.position + new Vector3((float)(Random.value - .5f) * 2.0f, transform.position.y, (float)(Random.value - .5f) * 2), Quaternion.identity);
                }
            }
        }
    }

}
