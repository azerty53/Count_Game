using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

	public int sens;
	public float speed;

    private Vector3 posXY;
    private Animator animator;

    private Animation _animation;
    private float clipLength;
    private bool goingIn;
    void Awake()
    {
        posXY = new Vector3(transform.position.x, transform.position.y,0);
        animator = transform.GetComponentInChildren<Animator>();
        
       
    }

	private void Move ()
	{
        posXY.x += sens * speed;
        transform.position = new Vector3(posXY.x, transform.position.y,0);
	}


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "House" && !goingIn)
        {
            sens = 0;
            animator.SetBool("Open", true);
            clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log(clipLength);
            StartCoroutine(GoIn());
            goingIn = true;
        }
    }

    IEnumerator GoIn()
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(this.gameObject);
    }

    void Update()
    {
        Move();
    }


}
