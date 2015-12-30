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
            if (instance == null) instance = FindObjectOfType(typeof(CharacterGenerator)) as CharacterGenerator;
            return instance;
        }

    }


    public List<GameObject> CharacterTypes;
    private List<Vector3> charPos = new List<Vector3>();
    public float rowsNumber;
    public float rowLength;
    public float rowWidth;
    public float backdoor=28;
    [HideInInspector]
    public int sens;
    [HideInInspector]
    public bool creationRunning = true;
    public List<GameObject> ListedWanderer = new List<GameObject>();
    public List<GameObject> ListedGuest = new List<GameObject>();
    public List<GameObject> ListedOnLeave = new List<GameObject>();

    private float minCreateSpeed, maxCreateSpeed, minReleaseSpeed, maxReleaseSpeed;
     
    private Vector3 charPosSource, charPosTemp;

    void Start()
    {      
        for (int i=0; i < rowsNumber; i++)
        {

            charPos.Add(new Vector3(rowLength,0, rowWidth * i));
            charPos.Add(new Vector3(-rowLength, 0, rowWidth * i));

        }
        minCreateSpeed = LevelManager.Instance.minCreateSpeed;
        maxCreateSpeed = LevelManager.Instance.maxCreateSpeed;
        minReleaseSpeed = LevelManager.Instance.minReleaseSpeed;
        maxReleaseSpeed = LevelManager.Instance.maxReleaseSpeed;

        StartRoutines();

    }

    void StartRoutines()
    {

        StartCoroutine(RandomWaitTime(CreateCharacter, minCreateSpeed, maxCreateSpeed));
        StartCoroutine(RandomWaitTime(ReleaseCharacter, minReleaseSpeed, maxReleaseSpeed));

    }


    IEnumerator RandomWaitTime(Action myMethodName,float minWaitingTime,float maxWaitingTime )
    {   
        yield return new WaitForSeconds(UnityEngine.Random.Range (minWaitingTime, maxWaitingTime));
        myMethodName();
    }

 
    public void CreateCharacter()
    {
        //If both doors are closed, no new wanderers will come the house's way
        if (!creationRunning)
        {
            StartCoroutine(RandomWaitTime(CreateCharacter, minCreateSpeed, maxCreateSpeed));
        }

        else
        {
            charPosTemp = charPos[UnityEngine.Random.Range(0, charPos.Count)];
            //Redirect all future guests to the open side of the house
            if ((!DoorsManager.Instance.doorsLeft && charPosTemp.x<0) || (!DoorsManager.Instance.doorsRight && charPosTemp.x>0))
            {
                charPosTemp.x *= -1;       
            }
            //Avoid having two consecutive wanderers on the same row 
            //If there are not, then allright...
            if (charPosTemp != charPosSource)
            {
                GameObject createdChar = Instantiate(CharacterTypes[UnityEngine.Random.Range(0, CharacterTypes.Count)], charPosTemp, Quaternion.identity) as GameObject;
                charPosSource = charPosTemp;
                createdChar.name = "Created Character";
                ListedWanderer.Add(createdChar);
                StartCoroutine(RandomWaitTime(CreateCharacter, minCreateSpeed, maxCreateSpeed));
            }
            //If they are tough, we rethrow the function until the random generator attributes him a differents row
            else CreateCharacter();
        }
    }


    public void ReleaseCharacter()
    {

        if (ListedGuest.Count > 0)
        {
            GameObject releaseChar = ListedGuest[UnityEngine.Random.Range(0, ListedGuest.Count)];
            ListedGuest.Remove(releaseChar);
            ListedOnLeave.Add(releaseChar);
            //Remove Money loan of character
            releaseChar.tag = "Out";
            releaseChar.name = "Release Character";
            releaseChar.transform.localPosition = new Vector3(0, 0, backdoor);
            releaseChar.SetActive(true);
            MoneyManager.Instance.Raise((releaseChar.GetComponentInChildren<CharacterBehavior>().moneyValue));

            HouseBehaviour.Instance.In--;
            StartCoroutine(RandomWaitTime(ReleaseCharacter, minReleaseSpeed, maxReleaseSpeed));
        }

        else StartCoroutine(RandomWaitTime(ReleaseCharacter, minReleaseSpeed, maxReleaseSpeed));


    }

 


    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.PlayState.Stop)
        {
            StopAllCoroutines();
        }
    }

   
}
