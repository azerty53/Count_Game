using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    protected GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
    }

    public enum PlayState { Play, Stop, Pause, Validate, CountDown };
    public PlayState gameState;
    public int stepsTime;
    public string buttonText="2X";

    public Text solution;

    void Awake()
    {
        ChangeTimeScale(1.0f);
        OnStart();

    }

   public void ChangeTimeScale (float speed)
    {
        Time.timeScale = speed;
    }

    public void OnStart()
    {
        gameState = PlayState.Play;
        ShowPanels.Instance.HideAll();
        ShowPanels.Instance.ShowInGame();
    }

    public void SpeedTime(float speed)
    {
        if (Time.timeScale < stepsTime * speed)
        {
            ChangeTimeScale(Time.timeScale *= speed);
           

            int temp = (int)(Time.timeScale * speed);
            buttonText = temp.ToString()+"X";

            if (Time.timeScale == stepsTime * speed)
            {
                buttonText = "Normal Speed";

            }
        }

        else
        {
            ChangeTimeScale(1.0f);
            buttonText = ((int)speed).ToString() + "X";

        }
        
    }

    public void CompareValue(int numberCounted)
    {
        gameState = PlayState.Validate;

        if (numberCounted == HouseBehaviour.Instance.In)
        {
            ShowPanels.Instance.HideAll();
            ShowPanels.Instance.ShowWin();
        }

        else
        {
            ShowPanels.Instance.HideAll();
            ShowPanels.Instance.ShowLose();
            solution.text = HouseBehaviour.Instance.In.ToString();
            
        }
        foreach (GameObject guests in CharacterGenerator.Instance.ListedGuest)
        {
            guests.tag = "Out";
            guests.SetActive(true);
        }
    }

    public void EndGame()
    {
        gameState = PlayState.Stop;
        ChangeTimeScale(1.0f);
        MoneyManager.Instance.BankClosed();
        ShowPanels.Instance.HideInGame();
        ShowPanels.Instance.ShowOnEnd();
        TurnBack();
    }

    public void Restart()
    {
        LevelManager.difficultyLvl = 0;
        gameState = PlayState.Play;
        Application.LoadLevel(0);

    }

    public void Continue()
    {
        LevelManager.Instance.IncreaseDifficuty();
        gameState = PlayState.Play;
        Application.LoadLevel(0);

    }

    public void TurnBack()
    {
        //foreach (GameObject ch in CharacterGenerator.Instance.ListedWanderer)
        //{

        //    ch.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));

        //}
    }
}
