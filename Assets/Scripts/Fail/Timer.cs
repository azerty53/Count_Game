using UnityEngine;
using System.Collections.Generic;
public class Timer : MonoBehaviour {

    
    private static Timer instance;
    private Timer() {}

        public static Timer Instance {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(Timer)) as Timer;
            }
            return instance;
        }
    }

    private List<float> Timers= new List<float>();
    private List<float> TimesLimit= new List<float>();

    private int number=0;

    public bool StartTime (float timeLimit)
    {

        TimesLimit[number] = timeLimit;

        
        Timers[number] += Time.deltaTime;
        if (Timers[number] >= TimesLimit[number])
        {
            Timers[number] = 0;
            return true;
        }
        else return false;

    }

    public void GiveTimer()
    {

        if (Timers.Count > 0)
        {
            for (int i = 0; Timers[i] != 0; i++)
            {
                if (i >= Timers.Count-1) { Timers.Add(0.0f); number = ++i;  break; }
                number = ++i;
                continue;
            }

        }
        else { Timers.Add(0.0f); TimesLimit.Add(0.0f); number = 0; }
    }
    


}
