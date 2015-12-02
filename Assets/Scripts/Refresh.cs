using UnityEngine;
using System.Collections.Generic;
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


        for (int i=0; i < CharacterGenerator.Instance.ListedGuest.Count; i++)
        {
            Destroy(CharacterGenerator.Instance.ListedGuest[i].gameObject);
            CharacterGenerator.Instance.ListedGuest.Remove(CharacterGenerator.Instance.ListedGuest[i]);
        }
         //chs.RemoveAll (delegate (GameObject o) { return o == null; });
    }
	

}
