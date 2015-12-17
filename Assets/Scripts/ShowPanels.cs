using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

    private static ShowPanels _instance;

    protected ShowPanels() { }

    public static ShowPanels Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(ShowPanels)) as ShowPanels;
            }
            return _instance;
        }
    }


    public GameObject inGame, onEnd, Win, Lose, Casheer;

    public void HideAll()
    {
        inGame.SetActive(false);
        onEnd.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
        Casheer.SetActive(false);
    }



    public void ShowInGame()
    {
        inGame.SetActive(true);
    }
    public void HideInGame()
    {
        inGame.SetActive(false);
    }

    public void ShowOnEnd()
    {
        onEnd.SetActive(true);
        Casheer.SetActive(true);
    }

    public void HideOnEnd()
    {
        onEnd.SetActive(false);
        Casheer.SetActive(false);
    }



    public void ShowWin()
    {
        Win.SetActive(true);
    }
    public void HideWin()
    {
        Win.SetActive(false);
    }


    public void ShowLose()
    {
        Lose.SetActive(true);
    }
    public void HideLose()
    {
        Lose.SetActive(false);
    }
}
