using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

   private Animator animator;
    private bool connected;
    void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "In" && GameManager.Instance.gameState == GameManager.PlayState.Play)
        {
            Debug.Log("Open Sesame");
            animator.SetBool("OpenB", true);

        }

    }

    void OnTriggerExit(Collider coll)

    {
        if (coll.tag == "In" || coll.tag == "Out" && GameManager.Instance.gameState == GameManager.PlayState.Play)
        {
            Debug.Log("Closing...");

            //animator.SetTrigger("Open");
            animator.SetBool("OpenB", false);

        }


    }


    void Update()
    {
       
        if (GameManager.Instance.gameState != GameManager.PlayState.Play)
        {
            animator.SetBool("OpenB", true);

        }

    }
}
