using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    private float prev_time;
    private Random rnd;
    // Start is called before the first frame update
    void Start()
    {
        prev_time = Time.time;
        rnd = new Random();

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - prev_time > 3.0f)
        {
            prev_time = Time.time;
            for(int i = 0; i < (int)(rnd.NextDouble() * 5 + 2); i++)
            {
                Instantiate(enemy, transform.position + new Vector3((float)(rnd.NextDouble() - .5f) * 2.0f, transform.position.y, (float)(rnd.NextDouble() - .5f) * 2), Quaternion.identity);
            }
        }
    }
}
