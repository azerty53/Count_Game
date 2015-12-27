using UnityEngine;
using System.Collections;

public class CopBehavior : MonoBehaviour {
    float clipLength;
    int copSense;
    CharacterBehavior copComponent;
    Animator thiefAnimator;
    void OnTriggerEnter (Collider coll)
    {

        if (coll.GetComponent<ThiefBehavior>())
        {
            CharacterBehavior thiefComponent = coll.GetComponent<CharacterBehavior>();
            copComponent = GetComponent<CharacterBehavior>();
            thiefAnimator = coll.GetComponentInChildren<Animator>();
            thiefComponent.sens = 0;
            copSense = copComponent.sens;
            copComponent.sens = 0;
            thiefAnimator.SetBool("Arrested", true);
            clipLength = thiefAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine("Arrested", coll.gameObject);

        }


    }

    IEnumerator Arrested(GameObject go)
    {
        yield return new WaitForSeconds(clipLength);
        CharacterGenerator.Instance.ListedWanderer.Remove(go);
        CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);

        Destroy(go);
        copComponent.sens = -copSense;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Destroy(GetComponent<BoxCollider>());

    }



}
