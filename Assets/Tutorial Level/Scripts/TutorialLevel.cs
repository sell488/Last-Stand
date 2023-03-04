using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    public GameObject[] tutorialObjects;
    public int currentTutorialIndex = 0;

    [SerializeField]
    private PlayerHealth player;
    private Firearm firearm;

    [Header("Movement parameters")]
    private bool[] movementBools;
    private bool WKey;
    private bool AKey;
    private bool SKey;
    private bool DKey;
    private bool jumpKey;
    private bool sprintKey;

    [Header("Weapon handling parameters")]
    private bool[] weaponHandlingBools;
    [SerializeField]
    WeaponSwitcher weaponSwitcher;

    private void Start()
    {
        buildMovementArray();
        buildWeaponTutorial();

        tutorialObjects[0].gameObject.SetActive(true);

        player = GetComponent<PlayerHealth>();
        weaponSwitcher = gameObject.GetComponent<WeaponSwitcher>();
        firearm = weaponSwitcher.currentGun.GetComponentInChildren<Firearm>();
    }

    private void Update()
    {
        if(currentTutorialIndex == 0)
        {
            if (hasDoneAllMovementOptions())
            {
                nextTutorial();
            }
        } else if(currentTutorialIndex == 1)
        {
            if(hasDoneAllBasicWeaponOptions())
            {
                nextTutorial();
            }
        } else if(currentTutorialIndex == 2)
        {
            if(hasDoneLongReload())
            {
                Invoke("nextTutorial", 3f);
            }
        } else if(currentTutorialIndex == 3)
        {
            if(hasSwitchedToNewWeapon())
            {
                nextTutorial();
            }
        } else if(currentTutorialIndex == 4)
        {
            if(hasChangedFireMode())
            {
                Invoke("nextTutorial", 5f);
            }
        } else if(currentTutorialIndex == 5)
        {
            if(hasChangedToShotgun())
            {
                nextTutorial();
            }
        } else if(currentTutorialIndex == 6)
        {
            if(hasShotFourTimes())
            {
                nextTutorial();
            }
        } else if(currentTutorialIndex == 7)
        {
            if(hasReloadedShotgun())
            {
                nextTutorial();               
            }
        } else if(currentTutorialIndex == 8)
        {
            //Shotgun reload mechanics
            Invoke("nextTutorial", 5f);
        } else if(currentTutorialIndex == 9)
        {
            nextTutorial();
        }
    }

    #region Movement
    private bool hasDoneAllMovementOptions()
    {
        bool isDone = true;

        if (Input.GetKeyDown(KeyCode.W))
        {
            //W Key
            movementBools[0] = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //A Key
            movementBools[1] = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //s Key
            movementBools[2] = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //D Key
            movementBools[3] = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Jump Key
            movementBools[4] = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Sprint Key
            movementBools[5] = true;
        }

        for (int index = 0; index < movementBools.Length; index++)
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
        movementBools = new bool[6];
    }
    #endregion

    #region Weapon Handling

    private bool hasDoneAllBasicWeaponOptions()
    {
        bool isDone = true;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Shooting
            weaponHandlingBools[0] = true;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Aiming
            weaponHandlingBools[1] = true;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Reloading
            weaponHandlingBools[2] = true;
        }

        for (int index = 0; index < weaponHandlingBools.Length; index++)
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
    #region Shotgun
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
    #endregion
    private void buildWeaponTutorial()
    {
        weaponSwitcher.guns[0].gameObject.SetActive(true);
        weaponHandlingBools = new bool[3];
    }

    #endregion


    private void nextTutorial()
    {
        tutorialObjects[currentTutorialIndex].SetActive(false);
        currentTutorialIndex++;
        tutorialObjects[currentTutorialIndex].SetActive(true);
    }
}
