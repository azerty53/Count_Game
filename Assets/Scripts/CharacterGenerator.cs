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


    public List<GameObject> CharacterTypes;
    private float[] AppValues = { 0f, .8f, .1f };
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
                             orderby element
                             select element;

        sortedAppValues = EnumerableList.ToList();
        Debug.Log(sortedAppValues[0]);
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
        //If both doors are closed, no new wanderers will go towards the doors'house
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
            //If there are not, then alright...
            if (charPosTemp != charPosSource)
            {
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

   

    void ChoseGOToInstantiate()
    {
        int lengthList = sortedAppValues.Count-1;
        int keyIndex;
        for (int i=0; i<=AppValues.Length-1; i++)
        {
            if (UnityEngine.Random.value<= sortedAppValues[lengthList-i])
            {
                keyIndex = sortedAppValues.FindIndex(w => w==AppValues[lengthList - i]);
                ToInstantiate = CharacterTypes[keyIndex];
                Debug.Log(keyIndex);

                break;
            }
            else { ToInstantiate = CharacterTypes[UnityEngine.Random.Range(0, CharacterTypes.Count)]; }
        }

    }
}
