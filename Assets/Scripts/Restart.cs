using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
