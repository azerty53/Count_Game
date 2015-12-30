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
            Debug.Log("Thief spotted");
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
        yield return new WaitForSeconds(clipLength+2.0f);
        CharacterGenerator.Instance.ListedWanderer.Remove(go);
        CharacterGenerator.Instance.ListedWanderer.Remove(gameObject);

        Destroy(go);
        copAnimator.transform.parent.transform.localRotation = Quaternion.Euler(new Vector3(0, 180 + copAnimator.transform.parent.transform.eulerAngles.y, 0));
        Debug.Log("Sens3 " + copComponent.sens);
        copAnimator.SetBool("Arresting", false);

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Destroy(GetComponent<BoxCollider>());

    }



}
