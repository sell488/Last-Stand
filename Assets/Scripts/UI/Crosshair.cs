using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private RectTransform _crosshair;

    [Range(50f, 250f)]
    public float restingSize;
    public float walkingSize;
    public float runningSize;
    public float speed;
    private float currentSize;
    private float targetSize;

    private void Start()
    {
        _crosshair = GetComponent<RectTransform>();
    }

    private void Update()
    {
        currentSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * speed);
        _crosshair.sizeDelta = new Vector2(currentSize, currentSize);
    }

    public void toRestingSize()
    {
        targetSize = restingSize;
    }

    public void toWalkingSize()
    {
        targetSize = walkingSize;
    }

    public void toRunningSize()
    {
        targetSize = runningSize;
    }

    public void toShootingPosition()
    {
        targetSize = runningSize;
        _crosshair.sizeDelta = new Vector2(runningSize, runningSize);
    }
}
