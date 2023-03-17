using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDamageUI : MonoBehaviour
{

    public float speed = 1;

    public Image fadingImage;

    public bool isFlashing;

    private Animator fadingAnim;

    // Start is called before the first frame update
    void Start()
    {
        fadingAnim = GetComponent<Animator>();
        isFlashing = false;
    }

    public IEnumerator OnBaseDamaged()
    {
        fadingAnim.Play("Base Health");
        isFlashing = true;
        yield return new WaitForSeconds(5f);
        fadingAnim.Play("Default");
        isFlashing = false;
    }

}
