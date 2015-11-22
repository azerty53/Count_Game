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
    private List<float> charPosY = new List<float>();
    private List<Vector3> charPos = new List<Vector3>();
    public float rowsNumber;
    public float RowWidth;
    public float RowLength;
    public int sens;

    private Vector3 charPosSource, charPosTemp;
    void Awake()
    {
        TimerSingle timer =gameObject.AddComponent<TimerSingle>();
        InvokeRepeating("CreateCharacter", 1.0f, Random.Range(2.0f, 3.0f));
        InvokeRepeating("ReleaseCharacter", 1.0f, Random.Range(2.0f, 3.0f));
        for (int i=0; i < rowsNumber; i++)
        {
           
            if (i < 2) { sens = 1; }
            else { sens = -1; }

            charPos.Add(new Vector3(RowWidth * sens,0, RowLength * i));
            
        }
    
    }

    public void CreateCharacter()
    {
         charPosTemp = charPos[Random.Range(0, charPos.Count)];
        if (charPosTemp != charPosSource)
        {
            GameObject createdChar;
            charPosSource = charPosTemp;
            ObjectCreator(out createdChar);
        }
        else CreateCharacter();

    }


    public void ReleaseCharacter()
    {
        if (HouseBehaviour.Instance.In > 0)
        {
            GameObject releaseChar;
            ObjectCreator(out releaseChar);
            HouseBehaviour.Instance.In --;
            releaseChar.transform.position = Vector3.zero;
           Destroy( releaseChar.GetComponent<BoxCollider>());
        }

    }

    public void ObjectCreator(out GameObject createdGameObject)
    {
        
        createdGameObject = Instantiate(CharacterTypes[Random.Range(0, CharacterTypes.Count)], charPosTemp, Quaternion.identity) as GameObject;

    }
}
