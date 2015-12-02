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

    void Start()
    {
        Chronometer.SetActive(false);
       timer = gameObject.AddComponent<TimerSingle>();
        chronoText = Chronometer.GetComponent<Text>();
        timeToComplete = LevelManager.Instance.timeLimit;

    }


    void Update()
    {
       
        if (!stop && timer.StartTimer(timeToComplete))
        {
            Debug.Log("Stop Game");
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
        Destroy(timer);
        chronoText.text = "Finish";
        GameManager.Instance.EndGame();
    }

    public void AddTimer()
    {
        timer = gameObject.AddComponent<TimerSingle>();

    }



}
