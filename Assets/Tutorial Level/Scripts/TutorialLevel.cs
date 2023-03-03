using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    public GameObject[] tutorialObjects;
    public int currentTutorialIndex = 0;

    [Header("Movement parameters")]
    private bool WKey = false;
    private bool AKey = false;
    private bool SKey = false;
    private bool DKey = false;



    private void Update()
    {
        #region Movement

        if(Input.GetKeyDown(KeyCode.W))
        {
            WKey = true;
        }

        #endregion
    }

    private void nextTutorial()
    {
        tutorialObjects[currentTutorialIndex].SetActive(false);
        currentTutorialIndex++;
        tutorialObjects[currentTutorialIndex].SetActive(true);
    }
}
