using UnityEngine;
using System.Collections;

public class DoorsManager : MonoBehaviour {

    private static DoorsManager _instance;

    protected DoorsManager() { }

    public static DoorsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(DoorsManager)) as DoorsManager;
            }
            return _instance;
        }
    }

    private bool _doorsRight = true;
    private bool _doorsLeft= true;

    public bool doorsRight 
    {
        get { return _doorsRight; }
        set { _doorsRight = value;
            if (!_doorsRight && !_doorsLeft)
            {
                CharacterGenerator.Instance.StopAllCoroutines();
            }
        }
    }

    public bool doorsLeft
    {
        get { return _doorsLeft; }
        set { _doorsLeft = value;
            if (!_doorsRight && !_doorsLeft)
            {
                CharacterGenerator.Instance.StopAllCoroutines();
            }
        }
    }

}
