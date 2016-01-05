using UnityEngine;
using System.Collections;

public class CopBehavior : MonoBehaviour
{
    float clipLength;
    int copSense;
    CharacterBehavior copComponent;
    Animator thiefAnimator, copAnimator;
    void OnTriggerEnter(Collider coll)
    {

        if (coll.gameObject.layer== LayerMask.NameToLayer("Thief"))
        {
            thiefAnimator = coll.GetComponent<Animator>();
            copAnimator = GetComponentInChildren<Animator>();

            copAnimator.SetBool("Arresting", true);
            thiefAnimator.SetBool("Arrested", true);
            clipLength = thiefAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine("Arrested", coll.gameObject);

        }


    }

    IEnumerator Arrested(GameObject go)
    {
        Destroy(go.GetComponent<BoxCollider>());
        yield return new WaitForSeconds(clipLength + 3.0f);
        CharacterGenerator.Instance.ListedWanderer.Remove(go);
        CharacterGenerator.Instance.ListedOnLeave.Remove(go);


        Destroy(go.transform.parent.gameObject);
        copAnimator.SetBool("Arresting", false);
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        //copComponent.tag = "Out";


    }



}
