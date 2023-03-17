using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGuide : MonoBehaviour
{

    [SerializeField]
    private GameObject guide1;
    [SerializeField] 
    private GameObject guide2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enabledGuide1()
    {
        guide1.SetActive(true);
        guide2.SetActive(false);
    }

    public void enabledGuide2()
    {
        guide1.SetActive(false);
        guide2.SetActive(true);
    }

    public void back()
    {
        guide1.SetActive(false);
        guide2.SetActive(false);
    }
}
