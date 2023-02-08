using System.Collections;
using UnityEngine;

// https://logicsimplified.com/newgames/how-to-build-a-first-person-shooter-fps-game-in-unity/
public class MouseLook : MonoBehaviour
{

    public float lookSensitivity = 3f, lookSmoothDamp = 0f;
    [HideInInspector]
    public float yRot, xRot;
    [HideInInspector]
    public float currentY, currentX;
    [HideInInspector]
    public float yRotationV, xRotationV;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yRot += Input.GetAxis("Mouse X") * lookSensitivity;
        xRot += Input.GetAxis("Mouse Y") * lookSensitivity;

        currentX = Mathf.SmoothDamp(currentX, xRot, ref xRotationV, lookSmoothDamp);
        currentY = Mathf.SmoothDamp(currentY, yRot, ref yRotationV, lookSmoothDamp);

        xRot = Mathf.Clamp(xRot, -80, 80);

        transform.rotation = Quaternion.Euler(-currentX, currentY, 0);
    }
}