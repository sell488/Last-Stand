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

    [SerializeField]
    protected GameObject playerDeathUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //text.text = "Health " + health.ToString();
        remainingHealth = health;
        //healthSlider = FindObjectOfType<UIPlayerHealth>();
        healthSlider.setHealth(100);
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
        StartCoroutine("HurtFlash");

        if (remainingHealth > 100)
        {
            damageE.a = 0;
            remainingHealth = 100;
        } else if(remainingHealth < 0)
        {
            remainingHealth = 0;
            playerDeathUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            GetComponent<MouseLook>().enabled = false;
        } else
        {
            float alphaValue = .85f - ((float)remainingHealth / ((float)health));
            if(alphaValue < 1 && alphaValue > 0)
            {
                damageE.a = alphaValue;
            }
            healthEffect.color = damageE;
        }
        healthSlider.setHealth(remainingHealth);

    }

    IEnumerator HurtFlash()
    {
        hurtEffect.enabled = true;
        yield return new WaitForSeconds(hurtEffectTime);
        hurtEffect.enabled = false;
    }
}
