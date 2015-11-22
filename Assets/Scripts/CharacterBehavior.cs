using UnityEngine;
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



    


    void Awake()
    {
        posXYZ = transform.position;
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

        if (posXYZ.x > 0) { sens = -1; transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); }

    }


	private void Move ()
	{
        posXYZ.x += sens * speed;
        transform.position = new Vector3(posXYZ.x, transform.position.y,transform.position.z);
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
        Destroy(gameObject);
        HouseBehaviour.Instance.In ++;
    }

    void Update()
    {

        Move();
      
    }


}
