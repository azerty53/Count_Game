using UnityEngine;
using System;

public class Boundaries : MonoBehaviour
{

    public void OnTriggerEnter (Collider coll)
    {

        if (coll.tag == "Out")
        {
            CharacterGenerator.Instance.ListedOnLeave.Remove(coll.gameObject);
        }
        if (coll.transform.parent.gameObject)
        {
            Destroy(coll.transform.parent.gameObject);
        }
        Destroy(coll.gameObject);
        
    }



}