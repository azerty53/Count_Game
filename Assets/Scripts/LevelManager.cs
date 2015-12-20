using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    private static LevelManager _instance;

    protected LevelManager() { }

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(LevelManager)) as LevelManager;
            }
            return _instance;
        }
    }

    [HideInInspector]
    public static int difficultyLvl=0;
    public List<float> timeLimits = new List<float>(3);
    [HideInInspector]
    public float timeLimit;

    public List<float> maxCreateSpeedsList, minCreateSpeedsList= new List<float>(3);
    [HideInInspector]
    public float maxCreateSpeed, minCreateSpeed;

    public List<float> maxReleaseSpeedsList, minReleaseSpeedsList= new List<float>(3);
    [HideInInspector]
    public float maxReleaseSpeed, minReleaseSpeed;


    private void Awake()
    {
        timeLimit = timeLimits[difficultyLvl];

        maxCreateSpeed = maxCreateSpeedsList[difficultyLvl];
        minCreateSpeed = minCreateSpeedsList[difficultyLvl];

        maxReleaseSpeed = maxReleaseSpeedsList[difficultyLvl];
        minReleaseSpeed = minReleaseSpeedsList[difficultyLvl];

    }

    public void IncreaseDifficuty()
    {
        difficultyLvl++;

        timeLimit = timeLimits[difficultyLvl];

        maxCreateSpeed = maxCreateSpeedsList[difficultyLvl];
        minCreateSpeed = minCreateSpeedsList[difficultyLvl];

        maxReleaseSpeed = maxReleaseSpeedsList[difficultyLvl];
        minReleaseSpeed = minReleaseSpeedsList[difficultyLvl];

    }





    public void IncreaseDifficulty(int level)
    {
        timeLimit = timeLimits[level];

        maxCreateSpeed = maxCreateSpeedsList[level];
        minCreateSpeed = minCreateSpeedsList[level];

        maxReleaseSpeed = maxReleaseSpeedsList[level];
        minReleaseSpeed = minReleaseSpeedsList[level];

    }

}
