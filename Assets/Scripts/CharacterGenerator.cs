using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {

    private static CharacterGenerator instance;
    protected CharacterGenerator() { }
    public static CharacterGenerator Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType(typeof(CharacterGenerator)) as CharacterGenerator;
            return instance;
        }

    }


    public List<GameObject> CharacterTypes;
    private List<Vector3> charPos = new List<Vector3>();
    public float rowsNumber;
    public float rowLength;
    public float rowWidth;
    [HideInInspector]
    public int sens;

    public List<GameObject> ListedWanderer = new List<GameObject>();
    public List<GameObject> ListedGuest = new List<GameObject>();
    public List<GameObject> ListedOnLeave = new List<GameObject>();

    private float minCreateSpeed, maxCreateSpeed, minReleaseSpeed, maxReleaseSpeed;
     
    private Vector3 charPosSource, charPosTemp;

    void Awake()
    {      
        for (int i=0; i < rowsNumber; i++)
        {
           
            if (i < (rowsNumber/2)) { sens = 1; }
            else { sens = -1; }

            charPos.Add(new Vector3(rowLength * sens,0, rowWidth * i));
            
        }
        minCreateSpeed = LevelManager.Instance.minCreateSpeed;
        maxCreateSpeed = LevelManager.Instance.maxCreateSpeed;
        minReleaseSpeed = LevelManager.Instance.minReleaseSpeed;
        maxReleaseSpeed = LevelManager.Instance.maxReleaseSpeed;

        StartRoutines();


    }

    IEnumerator RandomWaitTime(Action myMethodName,float minWaitingTime,float maxWaitingTime )
    {   
        yield return new WaitForSeconds(UnityEngine.Random.Range (minWaitingTime, maxWaitingTime));
        myMethodName();
    }

 
    public void CreateCharacter()
    {
         charPosTemp = charPos[UnityEngine.Random.Range(0, charPos.Count)];
        if ((!DoorsManager.Instance.doorsLeft && charPosTemp.x<0) || (!DoorsManager.Instance.doorsRight && charPosTemp.x>0))
        {
            charPosTemp.x *= -1;
        }

       
        if (charPosTemp != charPosSource)
        {
            GameObject createdChar = Instantiate(CharacterTypes[UnityEngine.Random.Range(0, CharacterTypes.Count)], charPosTemp, Quaternion.identity) as GameObject;
            charPosSource = charPosTemp;
            createdChar.name = "Created Character";
            ListedWanderer.Add(createdChar);
            StartCoroutine(RandomWaitTime(CreateCharacter, 1.0f, 5.0f));
        }
        else CreateCharacter();

    }


    public void ReleaseCharacter()
    {

        if (ListedGuest.Count > 0)
        {
            GameObject releaseChar = ListedGuest[UnityEngine.Random.Range(0, ListedGuest.Count)];
            ListedGuest.Remove(releaseChar);
            ListedOnLeave.Add(releaseChar);
            releaseChar.tag = "Out";
            releaseChar.name = "Release Character";
            releaseChar.SetActive(true);
            StartCoroutine(GoOut(releaseChar));
            HouseBehaviour.Instance.In--;
            StartCoroutine(RandomWaitTime(ReleaseCharacter, 5f, 15f));
        }

        else StartCoroutine(RandomWaitTime(ReleaseCharacter, 5f, 15f));


    }

    IEnumerator GoOut(GameObject releaseObject)
    {
        yield return new WaitForSeconds(15.0f);
       Destroy (releaseObject);

    }


    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.PlayState.Stop)
        {
            StopAllCoroutines();
        }
    }

    void StartRoutines()
    {
        
            StartCoroutine(RandomWaitTime(CreateCharacter, LevelManager.Instance.minCreateSpeed, LevelManager.Instance.maxCreateSpeed));
            StartCoroutine(RandomWaitTime(ReleaseCharacter, LevelManager.Instance.minReleaseSpeed, LevelManager.Instance.maxReleaseSpeed));
        
    }
}
