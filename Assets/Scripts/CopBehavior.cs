using UnityEngine;
using System.Collections;

public class CopBehavior : MonoBehaviour {
    float clipLength;
    int copSense;
    CharacterBehavior copComponent;
    Animator thiefAnimator, copAnimator;
    void OnTriggerEnter (Collider coll)
    {

        if (coll.GetComponent<ThiefBehavior>())
        {
            CharacterBehavior thiefComponent = coll.transform.parent.GetComponent<CharacterBehavior>();
            copComponent = GetComponent<CharacterBehavior>();
            thiefAnimator = coll.GetComponent<Animator>();
            copAnimator = GetComponentInChildren<Animator>();
            thiefComponent.sens = 0;
            copSense = copComponent.sens;
            Debug.Log("Sens1 "+copSense);
            copComponent.sens = 0;
            Debug.Log("Sens2 " + copComponent.sens);

            copAnimator.SetBool("Arresting", true);
            thiefAnimator.SetBool("Arrested", true);
            clipLength = thiefAnimator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine("Arrested", coll.gameObject);

        }


    }

    IEnumerator Arrested(GameObject go)
    {
        yield return new WaitForSeconds(clipLength+2.0f);
        CharacterGenerator.Instance.ListedWanderer.Remove(go);
        CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);

        Destroy(go);
        copComponent.sens = -copSense;
        Debug.Log("Sens3 " + copComponent.sens);
        copAnimator.SetBool("Arresting", false);

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Destroy(GetComponent<BoxCollider>());

    }



}
