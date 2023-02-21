using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{

    [SerializeField]
    private int waveSize;
    public GameObject enemy;

    private bool hasSpawned = false;

    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.GetComponent<PlayerHealth>() && !hasSpawned)
        {
            hasSpawned = true;
            for (int i = 0; i <= waveSize; i++)
            {
                Instantiate(enemy, spawnPoint.position + new Vector3((float)(Random.value - .5f) * 2.0f, spawnPoint.position.y, (float)(Random.value - .5f) * 2), Quaternion.identity);
            }

            GetComponentInParent<spawner>().shieldDown = true;
        }
    }

}
