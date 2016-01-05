using UnityEngine;


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
    public GameObject coin;
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


}
