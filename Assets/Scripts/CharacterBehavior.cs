﻿using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

    public int type;
    [HideInInspector]
	public int sens=1;
	private float speed;
    private string boolClip;

   
    private Vector3 posXYZ;
    private Animator animator;

    private float clipLength;
    private bool goingIn;
    private int [] sensValue= {-1,1 };

    void Awake()
    {
       
        animator = transform.GetComponentInChildren<Animator>();
        
       switch (type)
        {
            case 0: speed = 0.05f;
                    boolClip = "Open";
                break;
            case 1: speed = 1.0f;
                boolClip = "Open";
                break;
            default: goto case 0;

        }
    }

    void Start()
    {
        if (tag == "In")
        {
            posXYZ = transform.position;
            if (posXYZ.x > 0)
            {
                sens = -1;
                
            }
        }

        else
        {
            posXYZ = new Vector3 (0,0,28); Destroy(transform.GetComponent<BoxCollider>());
            sens = sensValue[Random.Range(0, 2)];
        }
        
        if (sens < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
    private void Move ()
	{
        posXYZ.x += sens * speed* Time.timeScale;

        transform.position = posXYZ;
	}


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "House" && !goingIn)
        {
            sens = 0;
            animator.SetBool(boolClip, true);
            clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(GoIn());
            goingIn = true;
        }
    }

    IEnumerator GoIn()
    {
        yield return new WaitForSeconds(clipLength);
        HouseBehaviour.Instance.In++;
        CharacterGenerator.Instance.ListedGuest.Add(gameObject);
        CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);
        Destroy(gameObject);
    }

    void Update()
    {

        Move();
      
    }


}
