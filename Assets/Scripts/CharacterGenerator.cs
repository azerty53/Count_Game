using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

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


    private List<GameObject> CharacterTypes= new List<GameObject>();
    public MyScriptableObject [] scriptableobjects;
    public Test []characters;
    private List<float> AppValues= new List<float>();
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
    // GO To Instantiate
    private GameObject ToInstantiate;
    private List<float> sortedAppValues;
    
    void Awake()
    {
        foreach(MyScriptableObject scrObj in scriptableobjects)
        {
            CharacterTypes.Add(scrObj.prefab);
            AppValues.Add(scrObj.apparitionValue);
        }
    }


    void Start()
    {
        //Create list of possible position for characters to appear; 
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


        //Collect information about chances of appearing for each character and put them in a list
        var EnumerableList = from element in AppValues
                             orderby element descending
                             select element;

        sortedAppValues = EnumerableList.ToList();
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
        
        if (creationRunning)
        {
            charPosTemp = charPos[UnityEngine.Random.Range(0, charPos.Count)];
            //Redirect all future guests to the open side of the house
            if ((!DoorsManager.Instance.doorsLeft && charPosTemp.x<0) || (!DoorsManager.Instance.doorsRight && charPosTemp.x>0))
            {
                charPosTemp.x *= -1;       
            }
            //Avoid having two consecutive wanderers on the same row 
            //If there are not, then alright...
            if (charPosTemp != charPosSource)
            {
                //According to the likeliness definition of each character the method will decide which one to spawn
                ChoseGOToInstantiate();
                GameObject createdChar = Instantiate(ToInstantiate, charPosTemp, Quaternion.identity) as GameObject;
                charPosSource = charPosTemp;
                createdChar.name = "Created Character";
                ListedWanderer.Add(createdChar);
                StartCoroutine(RandomWaitTime(CreateCharacter, minCreateSpeed, maxCreateSpeed));
            }
            //If they are, we rethrow the function until the random generator attributes him a differents row
            else CreateCharacter();
        }
        //If both doors are closed (DoorsManager script) or time is below X (TimeOut script), no new wanderers will go to the house

        else
        {
            StartCoroutine(RandomWaitTime(CreateCharacter, minCreateSpeed, maxCreateSpeed));
        }
    }


    public void ReleaseCharacter()
    {

        if (ListedGuest.Count > 0 && creationRunning)
        {
            GameObject releaseChar = ListedGuest[UnityEngine.Random.Range(0, ListedGuest.Count)];
            ListedGuest.Remove(releaseChar);
            ListedOnLeave.Add(releaseChar);
            //Remove Money loan of character
            releaseChar.transform.GetChild(0).tag = "Out";
            releaseChar.name = "Release Character";
            releaseChar.transform.localPosition = new Vector3(0, 0, backdoor);
            releaseChar.SetActive(true);
            MoneyManager.Instance.Raise((releaseChar.GetComponentInChildren<CharacterBehavior>().moneyValue));

            HouseBehaviour.Instance.In--;
            StartCoroutine(RandomWaitTime(ReleaseCharacter, minReleaseSpeed, maxReleaseSpeed));
        }
        //If both doors are closed (DoorsManager script) or time is below X (TimeOut script), no new wanderers will leave the house

        else StartCoroutine(RandomWaitTime(ReleaseCharacter, minReleaseSpeed, maxReleaseSpeed));


    }

 


    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.PlayState.Stop)
        {
            StopAllCoroutines();
        }
    }

   

    void ChoseGOToInstantiate()
    {
        int lengthList = sortedAppValues.Count-1;
        int keyIndex;
        for (int i=0; i<=AppValues.Count-1; i++)
        {
            if (UnityEngine.Random.value<= sortedAppValues[lengthList-i])
            {
                keyIndex = sortedAppValues.FindIndex(w => w==AppValues[lengthList - i]);
                ToInstantiate = CharacterTypes[keyIndex];

                break;
            }
            else { ToInstantiate = CharacterTypes[UnityEngine.Random.Range(0, CharacterTypes.Count-1)]; }
        }

    }
}
