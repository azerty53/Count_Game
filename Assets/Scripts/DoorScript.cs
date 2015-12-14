using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

   private Animator animator;


    void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();

    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "In" || coll.tag == "Out")
        {
            Debug.Log("Open Sesame");
            animator.SetBool("OpenB", true);

        }

    }

    void OnTriggerExit(Collider coll)

    {
        if (coll.tag == "In" || coll.tag == "Out")
        {
            Debug.Log("Closing...");

            //animator.SetTrigger("Open");
            animator.SetBool("OpenB", false);

        }


    }

}
