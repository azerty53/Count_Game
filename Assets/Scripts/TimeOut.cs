using UnityEngine;
using UnityEngine.UI;

public class TimeOut : MonoBehaviour {

    private static TimeOut _instance;

    protected TimeOut() { }

    public static TimeOut Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(TimeOut)) as TimeOut;
            }
            return _instance;
        }
    }



    TimerSingle timer;
    float timeToComplete;
    public GameObject Chronometer;
    private Text chronoText;
    private bool stop;
    void Awake()
    {
        timeToComplete = LevelManager.Instance.timeLimit;
        Chronometer.SetActive(false);
       timer = gameObject.AddComponent<TimerSingle>();
        chronoText = Chronometer.GetComponent<Text>();
    }


    void Update()
    {
       
        if (!stop && timer.StartTimer(timeToComplete))
        {
            StopGame();
        }

        if (timer.timerOut < 10.0f)
        {
            Chronometer.SetActive(true);
            chronoText.text = string.Format("{0:0}",timer.timerOut);
        }

    }

    void StopGame()
    {
        stop = true;
        GameManager.Instance.gameState = GameManager.PlayState.Stop;
        Destroy(timer);
        chronoText.text = "Finish";
        ShowPanels.Instance.HideInGame();
        ShowPanels.Instance.ShowOnEnd();
        foreach (GameObject ch in CharacterGenerator.Instance.ListedWanderer)
        {
            ch.GetComponent<CharacterBehavior>().sens *= -1;
        }
    }

    public void AddTimer()
    {
        timer = gameObject.AddComponent<TimerSingle>();

    }



}
