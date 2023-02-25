using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{

    private float gravity;
    private float velocity;
    private Vector3 startPos;
    private Vector3 startFor;

    private bool isInit = false;
    private float startTime = -1;

    public float mass;
    /// <summary>
    /// Should the bullet explode on impact?
    /// </summary>
    public bool isExplosive;

    public TMP_Text Velocity;

    /// <summary>
    /// Volume of a bullet.
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    public float Vb;

    /// <summary>
    /// Diameter of the bullet.
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    public float Db;

    /// <summary>
    /// Cross sectional area of a bullet.
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    public float A;

    /// <summary>
    /// Density of air (constant).
    /// </summary>
    /// <remarks>
    /// Used to calculate air drag
    /// </remarks>
    private float pa = 1.225f;

    public float Cd;

    /// <summary>
    /// Modifier for damage
    /// </summary>
    public float fudgeFactor;

    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<PlayerHealth>()) //&& !collision.GetComponent<Enemies>().isKilled)
        {
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(calculateDamage(velocity));
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private float calculateDamage(float vel)
    {
        print(100 * fudgeFactor * vel);
        return 100 * fudgeFactor * vel;
    }
}
