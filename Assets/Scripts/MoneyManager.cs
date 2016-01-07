using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
    private bool open = true;

    public List<GameObject> moneyDisplay;
    public Text text;
    public Animator animatorText;
    private float ones, tenths, hundreds;
    private float oneAngleTurn = 36.0f;
    bool startRotation;

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
            if (value > 0)
            {
                text.text = (value-_capital).ToString();
                animatorText.SetTrigger("Add");
                _capital = value;
                startRotation = true;
                StartCoroutine("RotateCounter");
            }
        }
    }
    IEnumerator RotateCounter()
    {
        if (_capital > 0)
        {
            int temp = (int)_capital % 10;
            float elapsedTime = 0;
            while (elapsedTime < 2.0f)
            {
                moneyDisplay[0].transform.rotation = Quaternion.Lerp(moneyDisplay[0].transform.rotation, Quaternion.AngleAxis(oneAngleTurn * temp, Vector3.left), elapsedTime);
                elapsedTime += 0.05f;
                yield return null;
             
            }
            if (_capital > 10)
            {
                int temp1 = (int)_capital / 10;
                float elapsedTime2 = 0;
                while (elapsedTime2 < 1.0f)
                {
                    moneyDisplay[1].transform.rotation = Quaternion.Lerp(moneyDisplay[1].transform.rotation, Quaternion.AngleAxis(oneAngleTurn * temp1, Vector3.left), elapsedTime2);
                    elapsedTime2 += 0.05f;
                    yield return null;

                }
                if (_capital > 100)
                {
                    int temp2 = (int)_capital / 100;
                    float elapsedTime3 = 0;
                    while (elapsedTime3 < 0.5f)
                    {
                        moneyDisplay[2].transform.rotation = Quaternion.Lerp(moneyDisplay[2].transform.rotation, Quaternion.AngleAxis(oneAngleTurn * temp2, Vector3.left), elapsedTime3);
                        elapsedTime3 += 0.05f;
                        yield return null;

                    }
                }
            }
        }
        yield return null;
    }
    public float fullCapital
    {
        get { return _fullCapital; }
        set{   _fullCapital = value; }
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
