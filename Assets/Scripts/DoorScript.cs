using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

   private Animator animator;
    public bool right;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

   public void OnTheClick()
    {
        if (animator.GetBool("OpenB"))
        {
            animator.SetBool("OpenB", false);
            if (right)
            {
                DoorsManager.Instance.doorsRight = false;
            }
            else {
                DoorsManager.Instance.doorsLeft = false;
            }
        }

        else
        {
            animator.SetBool("OpenB", true);

            if (right)
            {
                DoorsManager.Instance.doorsRight = true;
            }
            else {
                DoorsManager.Instance.doorsLeft = true;
            }
        }
    }


    void Update()
    {
       
        if (GameManager.Instance.gameState == GameManager.PlayState.Validate)
        {
            animator.SetBool("OpenB", true);

        }

    }
}
