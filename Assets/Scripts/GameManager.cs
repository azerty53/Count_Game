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
                _instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
    }

    public enum PlayState { Play, Stop, Pause };
    public PlayState gameState;
    public int stepsTime;
    public string buttonText="2X";

    public Text solution;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Time.timeScale = 1.0f;
        OnStart();
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
            Time.timeScale *= speed;

            int temp = (int)(Time.timeScale * speed);
            buttonText = temp.ToString()+"X";

            if (Time.timeScale == stepsTime * speed)
            {
                buttonText = "Normal Speed";

            }
        }

        else
        {
            Time.timeScale = 1.0f;
            buttonText = ((int)speed).ToString() + "X";

        }
        
    }

    public void CompareValue(int numberCounted)
    {
        if (numberCounted == HouseBehaviour.Instance.In)
        {
            ShowPanels.Instance.HideAll();
            ShowPanels.Instance.ShowWin();
            Debug.Log("You win");
        }

        else
        {
            ShowPanels.Instance.HideAll();
            ShowPanels.Instance.ShowLose();
            solution.text = HouseBehaviour.Instance.In.ToString();
            Debug.Log("You Lose");
            
        }
        foreach (GameObject guests in CharacterGenerator.Instance.ListedGuest)
        {
            Instantiate(guests, Vector3.zero, Quaternion.identity);
        }
    }

    public void Restart()
    {
        Debug.Log("Reload Level");
        LevelManager.Instance.difficultyLvl = 0;
        Application.LoadLevel(0);
        gameState = PlayState.Play;
        Refresh.Instance.RefreshScreen();

    }

    public void Continue()
    {

        Application.LoadLevel(0);
        LevelManager.Instance.IncreaseDifficuty();
        gameState = PlayState.Play;
        Refresh.Instance.RefreshScreen();


    }
}
