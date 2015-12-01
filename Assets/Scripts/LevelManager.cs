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

    
    public int difficultyLvl=0;
    public List<float> timeLimits = new List<float>(3);
    public float timeLimit;

    public List<float> maxCreateSpeedsList, minCreateSpeedsList= new List<float>(3);
    [HideInInspector]
    public float maxCreateSpeed, minCreateSpeed;

    public List<float> maxReleaseSpeedsList, minReleaseSpeedsList= new List<float>(3);
    [HideInInspector]
    public float maxReleaseSpeed, minReleaseSpeed;


    private void Awake()
    {
        maxCreateSpeed = maxCreateSpeedsList[difficultyLvl];
        minCreateSpeed = minCreateSpeedsList[difficultyLvl];

        maxReleaseSpeed = maxReleaseSpeedsList[difficultyLvl];
        minReleaseSpeed = minReleaseSpeedsList[difficultyLvl];

        timeLimit = timeLimits[difficultyLvl];
    }

    public void IncreaseDifficuty()
    {
        difficultyLvl++;
        maxCreateSpeed = maxCreateSpeedsList[difficultyLvl];
        minCreateSpeed = minCreateSpeedsList[difficultyLvl];

        maxReleaseSpeed = maxReleaseSpeedsList[difficultyLvl];
        minReleaseSpeed = minReleaseSpeedsList[difficultyLvl];

        timeLimit = timeLimits[difficultyLvl];
    }
        
        
        
        

    public void IncreaseDifficulty(int level)
    {


    }


}
