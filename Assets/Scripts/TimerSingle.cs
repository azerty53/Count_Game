using UnityEngine;
using System.Collections;

public class TimerSingle : MonoBehaviour {

    private float timer;

   public bool StartTimer(float timeLimit)
    {
        timer += Time.deltaTime;
        if (timer >= timeLimit)
        {
            timer = 0;
            return true;
        }
        else return false;
    }




}
