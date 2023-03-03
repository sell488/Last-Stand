using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAlert : MonoBehaviour
{

    private Animator anim;

    public static ReloadAlert reloadAlert;

    private void Start()
    {
        reloadAlert = this;
        anim = GetComponent<Animator>();
    }

    public void startReloadAlertInternal()
    {
        anim.Play("Reload Alert");
    }

    public void stopReloadAlertInternal()
    {
        anim.Rebind();
    }

    public static void startReloadAlert()
    {
        reloadAlert.startReloadAlertInternal();
    }

    public static void stopReloadAlert()
    {
        reloadAlert.stopReloadAlertInternal();
    }
}
