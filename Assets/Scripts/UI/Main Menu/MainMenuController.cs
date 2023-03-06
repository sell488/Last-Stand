using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Canvas homeCanvas;
    [SerializeField]
    private Canvas tutorialCanvas1;
    [SerializeField]
    private Canvas tutorialCanvas2;

    public void Play()
    {
        SceneManager.LoadScene("Stefan's Scene 2");
    }

    public void MechanicsTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Tutorial1()
    {
        tutorialCanvas1.gameObject.SetActive(true);
        tutorialCanvas2.gameObject.SetActive(false);
    }

    public void Tutorial2()
    {
        tutorialCanvas1.gameObject.SetActive(false);
        tutorialCanvas2.gameObject.SetActive(true);
    }

    public void Home()
    {
        tutorialCanvas1.gameObject.SetActive(false);
        tutorialCanvas2.gameObject.SetActive(false);
    }
}
