using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(checkHealth());
        
    }

    private IEnumerator checkHealth()
    {
        for(; ; ) {
            if (health <= 0)
            {
                print("Base destroyed");
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    /// <summary>
    /// Positive floats add health, negative floats subtract
    /// </summary>
    /// <param name="change"></param>
    public void changeHealth(float change)
    {
        if(0 <= health && health <= 100)
        {
            health += change;
        } else if(health <= 0)
        {
            Destroy(gameObject);
        }

        
    }
}
