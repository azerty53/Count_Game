using UnityEngine;
using System.Collections.Generic;
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
    public int sens;

    public List<GameObject> ListedWanderer = new List<GameObject>();
    public List<GameObject> ListedGuest = new List<GameObject>();
    public List<GameObject> ListedOnLeave = new List<GameObject>();


    private Vector3 charPosSource, charPosTemp;
    void Awake()
    {
        
        for (int i=0; i < rowsNumber; i++)
        {
           
            if (i < 2) { sens = 1; }
            else { sens = -1; }

            charPos.Add(new Vector3(rowLength * sens,0, rowWidth * i));
            
        }

        StartCreation();
    }


    public void StartCreation()
    {
        InvokeRepeating("CreateCharacter", 1.0f, Random.Range(5.0f, 10.0f));
        InvokeRepeating("ReleaseCharacter", 1.0f, Random.Range(10.0f, 15.0f));
    }


    public void CreateCharacter()
    {
         charPosTemp = charPos[Random.Range(0, charPos.Count)];
        if (charPosTemp != charPosSource)
        {
            GameObject createdChar;
            charPosSource = charPosTemp;
            ObjectCreator(out createdChar);
            createdChar.name = "Created Character";
            ListedWanderer.Add(createdChar);
            
        }
        else CreateCharacter();

    }


    public void ReleaseCharacter()
    {
        if (HouseBehaviour.Instance.In > 0)
        {
            GameObject releaseChar;
            ObjectCreator(out releaseChar);
            releaseChar.tag = "Out";
            releaseChar.name = "Release Character";
            ListedOnLeave.Add(releaseChar);
            HouseBehaviour.Instance.In--;
        }

    }

    public void ObjectCreator(out GameObject createdGameObject)
    { 
       createdGameObject = Instantiate(CharacterTypes[Random.Range(0, CharacterTypes.Count)], charPosTemp, Quaternion.identity) as GameObject;  
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.PlayState.Stop)
        {
            CancelInvoke();
        }
    }
}
