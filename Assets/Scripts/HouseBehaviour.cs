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
                _instance = GameObject.FindObjectOfType(typeof(HouseBehaviour))as HouseBehaviour;
            }
            return _instance;
        }
    }

    [HideInInspector]
    private  int _in;

    public int In 
        {
        get
        {
            return _in;
        }
        set
        {
            _in = value;
            Debug.Log(_in);
        }
        }


}
