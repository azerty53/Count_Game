using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

    public int type;
    [HideInInspector]
	public int sens=1;
    private int keepSens;
    //Unique Character propreties
	private float speed;
    private string boolClip;
    public float moneyValue;
    
   
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
                    moneyValue = 2.0f;
                break;
            case 1: speed = 0.1f;
                    boolClip = "Open";
                    moneyValue = 5.0f;
                break;
            default: goto case 0;

        }
    }

    void OnEnable()
    {
        posXYZ = transform.localPosition;

        if (tag == "In")
        {
            if (posXYZ.x > 0)
            {
                sens = -1;   
            }

            //Every Guest leaves the house (only when end of validation process)
            if (posXYZ.x == 0)
            {
                sens = sensValue[Random.Range(0, 2)];
            }

        }

        //When Characters are leaving
        else
        {
          
            sens = sensValue[Random.Range(0, 2)];
        }
        
       
        if (sens < 0) { transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);}
        else { transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); }
        
    }
    private void Move ()
	{
        posXYZ.x += sens * speed* Time.timeScale;

        transform.localPosition = posXYZ;
	}


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "House" && tag=="In")
        {
            if ((sens == -1 && !DoorsManager.Instance.doorsRight) || (sens == 1 && !DoorsManager.Instance.doorsLeft) || (!DoorsManager.Instance.doorsLeft && !DoorsManager.Instance.doorsRight))
            {
                sens *= -1;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);
                CharacterGenerator.Instance.ListedOnLeave.Add(gameObject);


            }
            else
            {
                sens = 0;
                animator.SetBool(boolClip, true);
                clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
                StartCoroutine(GoIn());
                //goingIn = true;
            }
        }

        if (coll.tag == "In")
        {
            speed = coll.transform.GetComponent<CharacterBehavior>().speed;
        }
    }

    IEnumerator GoIn()
    {
        yield return new WaitForSeconds(clipLength);
        MoneyManager.Instance.Raise(moneyValue);
        gameObject.SetActive(false);
        HouseBehaviour.Instance.In++;
        CharacterGenerator.Instance.ListedGuest.Add(gameObject);
        CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);
    }



    void Update()
    {

        Move();

    }

    //void OnBecameInvisible()
    //{
    //    gameObject.SetActive(false);
    //}

    //void OnBecameVisible()
    //{
    //    gameObject.SetActive(true);
    //}

   
}
