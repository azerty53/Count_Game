using UnityEngine;
using System.Collections.Generic;

public class HouseBehaviour : MonoBehaviour {

    private static HouseBehaviour _instance;

    protected HouseBehaviour() {}

    public static HouseBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(HouseBehaviour))as HouseBehaviour;
            }
            return _instance;
        }
    }
    private  int _in;
    public List<GameObject> VisualFeedbacksFront;
    public List<GameObject> VisualFeedbacksBack;
    public List <GameObject> placesFront;
    public List<GameObject> placesBack;
    private GameObject [] visuFront= new GameObject[2];
    private GameObject[] visuBack = new GameObject[2];
    private List<Animator> animators= new List<Animator>();
    public GameObject folder;
    public int level;
    void Awake()
    {
        GameObject folderVisu = new GameObject();
        folderVisu.name = "Visu";
        for (int i = 0; i < 2; i++)
        {
            visuFront[i] = Instantiate(VisualFeedbacksFront[level], placesFront[i].transform.position, Quaternion.identity) as GameObject;
            visuFront[i].name = "visuFront" + i;
            visuFront[i].transform.parent = folderVisu.transform;
            animators.Add(visuFront[i].GetComponentInChildren<Animator>());
            visuBack[i] = Instantiate(VisualFeedbacksBack[level], placesBack[i].transform.position, Quaternion.identity) as GameObject;
            visuBack[i].name = "visuBack" + i;
            visuBack[i].transform.parent = folderVisu.transform;
             animators.Add(visuBack[i].GetComponentInChildren<Animator>());
        }
        folderVisu.transform.parent = folder.transform;
    }
    public int In 
        {
        get
        {
            return _in;
        }
        set
        {
            _in = value;
        }
        }
    public void CreateFeedback (int right, bool In)
    {
        if (In)
        {
            if (right==-1)
            {
               animators[0].SetTrigger("Flip");
            }
            else
            {
                animators[2].SetTrigger("Flip");
            }
        }
        else
        {
            if (right==-1)
            {   
                animators[1].SetTrigger("Flip");
            }

            else
            {
                animators[3].SetTrigger("Flip");
            }
        }
    }
}
