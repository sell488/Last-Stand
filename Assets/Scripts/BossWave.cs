using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWave : MonoBehaviour
{

    [SerializeField]
    private int waveSize;
    public GameObject enemy;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.GetComponent<PlayerHealth>() && !hasSpawned)
        {
            hasSpawned = true;
            print("player has entered the collider");
            for (int i = 0; i <= waveSize; i++)
            {
                Instantiate(enemy, transform.position + new Vector3((float)(Random.value - .5f) * 2.0f, transform.position.y, (float)(Random.value - .5f) * 2), Quaternion.identity);
            }
            Destroy(gameObject.GetComponentInParent<GameObject>());
            gameObject.SetActive(false);
        }
    }

}
