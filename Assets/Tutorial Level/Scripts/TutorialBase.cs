using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBase : Base
{
    [SerializeField]
    private TMP_Text baseDamage;
    private void Start()
    {
        moderateDamage.Stop();
        criticalDamage.Stop();
    }
    public override void changeHealth(float change)
    {
        if (0 <= health && health <= 100)
        {
            if (!damageUI.isFlashing)
            {
                StartCoroutine("baseDamageHint");
                damageUI.StartCoroutine("OnBaseDamaged");
            }
            health += change;
            if (!moderateDamageEnabled && health <= 50 && health > 25)
            {
                enabledModerateDamage();
            }
            else if (!criticalDamageEnabled && health <= 25)
            {
                enabledCriticalDamage();
            }
        }
        else if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator baseDamageHint()
    {
        baseDamage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        baseDamage.gameObject.SetActive(false);
    }
}
