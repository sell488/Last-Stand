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

    public TMP_Text wavesSurvivedUI;

    private int wavesSurvived = 0;

    private int spawnersKilled = 0;

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
        scoreDisplay.text = "Score: " + killCount.ToString();
    }

    public static void KilledSpawner()
    {
        Singleton.KilledSpawnerInternal();
    }

    private void KilledSpawnerInternal()
    {
        spawnersKilled++;
    }

    public void setScoreDisplay(string text)
    {
        Singleton.scoreDisplay.text = text;
    }

    public static void waveSurvived()
    {
        Singleton.waveSurvivedInternal();
    }

    public void waveSurvivedInternal()
    {
        wavesSurvived++;
        wavesSurvivedUI.text = "Waves Survived: " + (wavesSurvived - 1).ToString();
    }

    public static int getScore()
    {
        return Singleton.killCount;
    }

    public static int GetSpawnersKilled()
    {
        return Singleton.spawnersKilled;
    }

}
