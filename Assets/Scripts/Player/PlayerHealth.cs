using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    /// <summary>
    /// How much health the player starts out with
    /// </summary>
    public int health;

    [HideInInspector]
    public float remainingHealth;

    public TMP_Text text;

    private UIPlayerHealth healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        text.text = "Heatlh " + health.ToString();
        remainingHealth = health;
        healthSlider = FindObjectOfType<UIPlayerHealth>();
        healthSlider.setHealth(remainingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Health: " + remainingHealth.ToString();
    }

    public void takeDamage(float damage)
    {
        remainingHealth -= damage;

        if(remainingHealth > 100)
        {
            remainingHealth = 100;
        }
        if(remainingHealth < 0)
        {
            remainingHealth = 0;
        }

        healthSlider.setHealth(remainingHealth);

    }
}
