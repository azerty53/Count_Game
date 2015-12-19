using UnityEngine;

public class TimerSingle : MonoBehaviour {
    [HideInInspector]
    public float timerOut;
    private float timer;

    private bool triggered;

   public bool StartTimer(float timeLimit)
    {
        if (!triggered) {

            timer += Time.deltaTime;
            timerOut = timeLimit - timer;
        }
        
        if (timerOut<=0)
        {
            triggered = true;
            timer = 0;
            timerOut = timeLimit;
            triggered = false;
            return true;

        }
        else return false;
    }




}
