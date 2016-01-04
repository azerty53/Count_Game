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

        if (coll.GetComponent<ThiefBehavior>())
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
        yield return new WaitForSeconds(clipLength + 4.0f);
        CharacterGenerator.Instance.ListedWanderer.Remove(go);
        CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);

        Destroy(go.transform.parent.gameObject);
        copAnimator.SetBool("Arresting", false);
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        //copComponent.tag = "Out";


    }



}
