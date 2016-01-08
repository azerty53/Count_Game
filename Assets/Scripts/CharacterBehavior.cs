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
    [HideInInspector]
    public float moneyValue;
    //[HideInInspector]
    //public float apparitionValue;
   
    private Vector3 posXYZ;
    private Animator animator;

    private float clipLength;
    private bool goingIn;
    private int [] sensValue= {-1,1 };
    private GameObject Parent;
    void Awake()
    {
        Parent = transform.parent.gameObject;
        animator = transform.GetComponent<Animator>();
        MyScriptableObject scrObj = CharacterGenerator.Instance.scriptableobjects[type];
        //switch (type)
        //{

        //    case 0:
        //        speed = 0f;
        //        boolClip = "Open";
        //        moneyValue = 2.0f;
        //        break;
        //    case 1:
        //        speed = 0.1f;
        //        boolClip = "Open";
        //        moneyValue = 5.0f;

        //        break;
        //    case 2:
        //        speed = 0f;
        //        boolClip = "Open";
        //        moneyValue = -2.0f;
        //        break;
        //    default: goto case 0;

        //}
        boolClip = scrObj.openAnim;
        moneyValue = scrObj.moneyValue;

    }

    void OnEnable()
    {
        posXYZ = Parent.transform.localPosition;

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
            if (gameObject.layer == LayerMask.NameToLayer("Thief")) 
            {
                animator.SetBool("Leaving", true);
            }

        }


        if (sens < 0) { Parent.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0)); }
        else { Parent.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); }
        
    }
 


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "House" && tag=="In")
        {
            if ((sens == -1 && !DoorsManager.Instance.doorsRight) || (sens == 1 && !DoorsManager.Instance.doorsLeft) || (!DoorsManager.Instance.doorsLeft && !DoorsManager.Instance.doorsRight))
            {

                transform.localRotation = Quaternion.Euler(new Vector3(0,-90, 0));

                CharacterGenerator.Instance.ListedWanderer.Remove(Parent.gameObject);
                CharacterGenerator.Instance.ListedOnLeave.Add(Parent.gameObject);
              

            }
            else
            {
                animator.SetBool(boolClip, true);
                clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
                StartCoroutine(GoIn());
            }
        }

        //if (coll.tag == "In")
        //{
        //    speed = coll.transform.GetComponent<CharacterBehavior>().speed;
        //}
    }

    IEnumerator GoIn()
    {
        HouseBehaviour.Instance.CreateFeedback(sens, true);
        yield return new WaitForSeconds(1.0f);
        MoneyManager.Instance.Raise(moneyValue);
        Parent.SetActive(false);
        transform.position = new Vector3(0, transform.position.y, 0);
        Parent.transform.position = Vector3.zero;
        HouseBehaviour.Instance.In++;
        CharacterGenerator.Instance.ListedGuest.Add(Parent.gameObject);
        CharacterGenerator.Instance.ListedWanderer.Remove(Parent.gameObject);

    }


}
