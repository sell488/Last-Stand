using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    public GameObject[] tutorialObjects;
    public int currentTutorialIndex = 0;

    private PlayerHealth player;
    private Firearm firearm;

    [Header("Movement parameters")]
    private bool[] movementBools = new bool[6];
    private bool WKey = false;
    private bool AKey = false;
    private bool SKey = false;
    private bool DKey = false;
    private bool jumpKey = false;
    private bool sprintKey = false;

    [Header("Weapon handling parameters")]
    private bool[] weaponHandlingBools = new bool[5];
    private bool hasShot = false;
    private bool hasAimed = false;
    private bool hasReloaded = false;
    private bool hasSwitchedWeapon = false;
    private bool hasSwitchedFireMode = false;
    WeaponSwitcher weaponSwitcher;

    private void Start()
    {
        buildMovementArray();
        buildWeaponTutorial();

        player = FindObjectOfType<PlayerHealth>();
        firearm = player.GetComponentInChildren<Firearm>();
        weaponSwitcher = player.GetComponent<WeaponSwitcher>();
    }

    private void Update()
    {
        if(currentTutorialIndex == 0)
        {
            if (hasDoneAllMovementOptions())
            {
                nextTutorial();
                currentTutorialIndex++;
            }
        } else if(currentTutorialIndex == 1)
        {
            if(hasDoneAllBasicWeaponOptions())
            {
                nextTutorial();
                currentTutorialIndex++;
            }
        } else if(currentTutorialIndex == 2)
        {
            if(hasDoneLongReload())
            {
                nextTutorial();
                currentTutorialIndex++;
            }
        } else if(currentTutorialIndex == 3)
        {
            if(hasSwitchedToNewWeapon())
            {
                nextTutorial();
                currentTutorialIndex++;
            }
        } else if(currentTutorialIndex == 4)
        {
            if(hasChangedFireMode())
            {
                Invoke("waitBeforeMovingOn", 5f);
            }
        } else if(currentTutorialIndex == 5)
        {
            if(hasChangedToShotgun())
            {
                nextTutorial();
                currentTutorialIndex++;
            }
        } else if(currentTutorialIndex == 6)
        {
            if(hasShotFourTimes())
            {
                nextTutorial();
                currentTutorialIndex++;
            }
        } else if(currentTutorialIndex == 7)
        {
            if(hasReloadedShotgun())
            {
                nextTutorial();
                currentTutorialIndex++;                
            }
        } else if(currentTutorialIndex == 8)
        {
            Invoke("waitBeforeMovingOn", 5f);
        } else if(currentTutorialIndex == 9)
        {

        }
    }

    private void waitBeforeMovingOn()
    {
        currentTutorialIndex++;
        nextTutorial();
    }

    #region Movement
    private bool hasDoneAllMovementOptions()
    {
        bool isDone = true;

        if (Input.GetKeyDown(KeyCode.W))
        {
            WKey = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            AKey = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            SKey = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            DKey = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpKey = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintKey = true;
        }

        for (int index = 0; index < tutorialObjects.Length; index++)
        {
            if (!movementBools[index])
            {
                isDone = false;
            }
        }

        return isDone;
    }

    private void buildMovementArray()
    {
        movementBools[0] = WKey;
        movementBools[1] = AKey;
        movementBools[2] = SKey;
        movementBools[3] = DKey;
        movementBools[4] = jumpKey;
        movementBools[5] = sprintKey;
    }
    #endregion

    #region Weapon Handling

    private bool hasDoneAllBasicWeaponOptions()
    {
        bool isDone = true;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            hasShot = true;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            hasAimed = true;
        }

        for (int index = 0; index < 2; index++)
        {
            if (!weaponHandlingBools[index])
            {
                isDone = false;
            }
        }

        return isDone;
    }

    private bool hasDoneLongReload()
    {
        if (!(firearm.magRounds > 0))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                return true;
            }
        }
        return false;
    }

    private bool hasSwitchedToNewWeapon()
    {
        weaponSwitcher.guns[1].GetComponentInChildren<Firearm>().isBought = true;
        if (weaponSwitcher.guns[1].activeInHierarchy)
        {
            return true;
        }
        return false;
    }

    private bool hasChangedFireMode() {
        if(Input.GetKeyDown(KeyCode.V))
        {
            return true;
        }
        return false;
    }

    private bool hasChangedToShotgun()
    {
        weaponSwitcher.guns[2].GetComponentInChildren<Firearm>().isBought = true;
        if (weaponSwitcher.guns[2].activeInHierarchy)
        {
            firearm = weaponSwitcher.guns[2].GetComponentInChildren<Firearm>();
            return true;
        }
        return false;
    }

    private bool hasShotFourTimes()
    {
        if (firearm.magRounds < (firearm.magCount - 3))
        {
            return true;
        }
        return false;
    }

    private bool hasReloadedShotgun()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            return true;
        }
        return false;
    }

    private void buildWeaponTutorial()
    {
        weaponSwitcher.guns[0].gameObject.SetActive(true);

        weaponHandlingBools[0] = hasShot;
        weaponHandlingBools[1] = hasAimed;
        weaponHandlingBools[2] = hasReloaded;
        weaponHandlingBools[3] = hasSwitchedWeapon;
        weaponHandlingBools[4] = hasSwitchedFireMode;
    }

    #endregion

    private void nextTutorial()
    {
        tutorialObjects[currentTutorialIndex].SetActive(false);
        currentTutorialIndex++;
        tutorialObjects[currentTutorialIndex].SetActive(true);
    }
}
