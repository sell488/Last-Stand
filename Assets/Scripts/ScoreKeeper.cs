using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(TMP_Text))]
public class ScoreKeeper : MonoBehaviour
{

    /// <summary>
    /// Current score
    /// </summary>
    public int killCount;

    /// <summary>
    /// There will only ever be one ScoreKeeper object, so we just store it in
    /// a static field so we don't have to call FindObjectOfType(), which is expensive.
    /// </summary>
    public static ScoreKeeper Singleton;

    /// <summary>
    /// Text component for displaying the score
    /// </summary>
    private TMP_Text scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        scoreDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        ScorePointsInternal(0);
    }

    /// <summary>
    /// Add points to the score
    /// </summary>
    /// <param name="points">Number of points to add to the score; can be positive or negative</param>
    public static void ScorePoints(int points)
    {
        Singleton.ScorePointsInternal(points);
    }

    private void ScorePointsInternal(int delta)
    {
        killCount += delta;
        scoreDisplay.text = killCount.ToString();
    }

    public static void setScoreDisplay(string text)
    {
        Singleton.scoreDisplay.text = text;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
