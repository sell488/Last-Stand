using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    /// <summary>
    /// How much health the player starts out with
    /// </summary>
    public int health;

    public float remainingHealth;

    [SerializeField]
    private Image healthEffect = null;

    [SerializeField]
    private Image hurtEffect;

    [SerializeField]
    private float hurtEffectTime;

    public TMP_Text text;
    [SerializeField]
    private UIPlayerHealth healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //text.text = "Health " + health.ToString();
        remainingHealth = health;
        //healthSlider = FindObjectOfType<UIPlayerHealth>();
        healthSlider.setHealth(remainingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = "Health: " + remainingHealth.ToString();
    }

    public void takeDamage(float damage)
    {
        remainingHealth -= damage;
        Color damageE = healthEffect.color;

        if (remainingHealth > 100)
        {
            damageE.a = 0;
            remainingHealth = 100;
        } else if(remainingHealth < 0)
        {
            remainingHealth = 0;
            //SceneManager.LoadScene(1);
        } else
        {
            damageE.a = 1f - (float)remainingHealth / (float)health;
            healthEffect.color = damageE;
            //HurtFlash();
        }
        print(remainingHealth);
        healthSlider.setHealth(remainingHealth);

    }

    IEnumerator HurtFlash()
    {
        hurtEffect.enabled = true;
        yield return new WaitForSeconds(hurtEffectTime);
        hurtEffect.enabled = false;
    }
}
