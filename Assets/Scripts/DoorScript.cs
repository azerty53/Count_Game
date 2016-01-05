using UnityEngine;

public class DoorScript : MonoBehaviour
{

    private Animator[] animators = new Animator[2];
    public bool right;
    public GameObject[] doors;
    void Awake()
    {
        for (int i=0; i < doors.Length; i++)
        {
            animators[i] = doors[i].transform.GetComponent<Animator>();
        }
    }

   public void OnTheClick()
    {
        if (animators[0].GetBool("OpenB"))
        {
            foreach(Animator anim in animators)
            {
                anim.SetBool("OpenB", false);
                if (right)
                {
                    DoorsManager.Instance.doorsRight = false;
                }
                else
                {
                    DoorsManager.Instance.doorsLeft = false;
                }

            }
           
        }

        else
        {
            foreach (Animator anim in animators)
            {
                anim.SetBool("OpenB", true);
                if (right)
                {
                    DoorsManager.Instance.doorsRight = true;
                }
                else
                {
                    DoorsManager.Instance.doorsLeft = true;
                }

            }
        }
    }


    void Update()
    {

        if (GameManager.Instance.gameState == GameManager.PlayState.Validate)
        {

            foreach (Animator anim in animators)
            {
                anim.SetBool("OpenB", true);
                DoorsManager.Instance.doorsRight = true;
                DoorsManager.Instance.doorsLeft = true;
            }

        }

        else if (GameManager.Instance.gameState == GameManager.PlayState.CountDown)
        {

            foreach (Animator anim in animators)
            {
                anim.SetBool("OpenB", false);
                DoorsManager.Instance.doorsRight = false;
                DoorsManager.Instance.doorsLeft = false;
            }

        }

    }
}
