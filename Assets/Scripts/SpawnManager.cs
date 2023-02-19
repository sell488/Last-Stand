using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public spawner[] spawner;
    /// <summary>
    /// How long spawners should be enabled for each wave
    /// </summary>
    public float waveEnabledTime;
    /// <summary>
    /// The remaining time until the next wave
    /// </summary>
    private float timeUntilNextWave;
    /// <summary>
    /// How much time there should be between waves. 
    /// <strong>THIS SHOULD ALWAYS BE GREATER THAN waveEnabledTime!</strong>
    /// </summary>
    public float initTimeUntilNextWave;

    public TMP_Text UITimer;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextWave = initTimeUntilNextWave;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilNextWave <= 0)
        {
            StartCoroutine(spawnWave());
            timeUntilNextWave = initTimeUntilNextWave;
        } else
        {
            timeUntilNextWave -= Time.deltaTime;
        }
        UITimer.text = ((int)timeUntilNextWave).ToString();

    }

    IEnumerator spawnWave()
    {
        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].spawningEnabled = true;
        }
        yield return new WaitForSeconds(waveEnabledTime);
        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].spawningEnabled = false;
        }
        
    }
}
