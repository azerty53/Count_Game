using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour
{

    private static MoneyManager _instance;

    protected MoneyManager() { }

    public static MoneyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(MoneyManager)) as MoneyManager;
            }
            return _instance;
        }
    }


    private float _money;
    private float _capital;
    private float _totalRaise;
    protected float _fullCapital;
    private TimerSingle timer;
    private bool open= true;
    void OnEnable()
    {
        timer = GetComponent<TimerSingle>();
    }

    public float money
    {
        get { return _money; }
        set { _money = value; }

    }

    public float totalRaise
    {
        get { return _totalRaise; }
        set
        {
            _totalRaise = value;
            if (_totalRaise < 0)
            {
                Debug.Log("Losing Money");
            }
        }
    }

    public float capital
    {
        get { return _capital; }
        set
        {
            _capital = value;
            Debug.Log(capital);
        }

    }

    public float fullCapital
    {
        get { return _fullCapital; }
        set { _fullCapital = value; }
    }

    public void Raise(float increase)
    {
        totalRaise += increase;
    }

    public void Update()
    {
        if (open)
        {
            if (timer.StartTimer(2.0f))
            {
                capital += totalRaise;
            }
        }
    }

    public void BankClosed()
    {
        open = false;
        totalRaise = 0;
        fullCapital += capital;
        capital = 0;
    }
    

}
