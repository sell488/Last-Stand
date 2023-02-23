using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{

    public Slider slider;
    public Image sliderImage;
    private Color healthyColor;
    private Color damagedColor;
    private PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        healthyColor = Color.green; 
        damagedColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float remainingHealth)
    {
        slider.value = remainingHealth;
        sliderImage.color = Color.Lerp(damagedColor, healthyColor, remainingHealth / 100f);
    }
}
