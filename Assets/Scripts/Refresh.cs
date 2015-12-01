using UnityEngine;

public class Refresh : MonoBehaviour {

    private static Refresh _instance;

    protected Refresh() { }

    public static Refresh Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(Refresh)) as Refresh;
            }
            return _instance;
        }
    }


   public void RefreshScreen ()
    {
        TimeOut.Instance.AddTimer();

    }
	

}
