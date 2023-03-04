using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Base : MonoBehaviour
{

    public float health;

    public VisualEffect moderateDamage;
    public VisualEffect criticalDamage;

    protected bool moderateDamageEnabled = false;
    protected bool criticalDamageEnabled = false;

    public BaseDamageUI damageUI;

    // Start is called before the first frame update
    void Start()
    {
        moderateDamage.Stop();
        criticalDamage.Stop();
        damageUI = FindObjectOfType<BaseDamageUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(checkHealth());
        
    }

    private IEnumerator checkHealth()
    {
        while(true) {
            if (health <= 0)
            {
                SceneManager.LoadScene(1);
                print("Base destroyed");
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    /// <summary>
    /// Positive floats add health, negative floats subtract
    /// </summary>
    /// <param name="change"></param>
    public virtual void changeHealth(float change)
    {
        if(0 <= health && health <= 100)
        {
            if (!damageUI.isFlashing)
            {
                damageUI.StartCoroutine("OnBaseDamaged");
            }
            health += change;
            if(!moderateDamageEnabled && health <= 50 && health > 25)
            {
                enabledModerateDamage();
            } else if(!criticalDamageEnabled && health <= 25)
            {
                enabledCriticalDamage();
            }
        } else if(health <= 0)
        {
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
    }

    public void enabledModerateDamage()
    {
        moderateDamageEnabled = true;
        moderateDamage.Play();
    }

    public void enabledCriticalDamage()
    {
        criticalDamageEnabled = true;
        moderateDamage.Stop();
        criticalDamage.Play();
    }
}
