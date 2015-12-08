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

        for (int i = 0; i < CharacterGenerator.Instance.ListedOnLeave.Count; i++)
        {
            Destroy(CharacterGenerator.Instance.ListedOnLeave[i].gameObject);
            CharacterGenerator.Instance.ListedOnLeave.Remove(CharacterGenerator.Instance.ListedOnLeave[i]);
        }

        for (int i = 0; i < CharacterGenerator.Instance.ListedWanderer.Count; i++)
        {
            Destroy(CharacterGenerator.Instance.ListedWanderer[i].gameObject);
            CharacterGenerator.Instance.ListedWanderer.Remove(CharacterGenerator.Instance.ListedWanderer[i]);
        }


        ShowPanels.Instance.HideAll();
        ShowPanels.Instance.ShowInGame();
        GameManager.Instance.gameState = GameManager.PlayState.Play;

        HouseBehaviour.Instance.In = 0;
    }
	

}
