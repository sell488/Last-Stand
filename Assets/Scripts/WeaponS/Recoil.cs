using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    //Rotations
    private Vector3 currentRot;
    private Vector3 targetRot;

    //Hipfire recoil
    [SerializeField]
    private float recoilX;
    [SerializeField]
    private float recoilY;
    [SerializeField]
    private float recoilZ;

    //Settings
    [SerializeField]
    private float snappiness;
    [SerializeField]
    private float returnSpeed;

    public Crosshair _crosshair;

    void Start()
    {
        
    }

    void OnEnable()
    {
        Firearm.OnShoot += RecoilFire;
    }

    private void OnDisable()
    {
        Firearm.OnShoot -= RecoilFire;
    }

    void Update()
    {
        targetRot = Vector3.Lerp(targetRot, Vector3.zero, returnSpeed * Time.deltaTime);

        currentRot = Vector3.Slerp(currentRot, targetRot, snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRot);
    }

    public void RecoilFire()
    {
        targetRot += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        _crosshair.toShootingPosition();
    }


}
