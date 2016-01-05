using UnityEngine;
using System;

public class Boundaries : MonoBehaviour
{

    public void OnTriggerEnter (Collider coll)
    {

       
            CharacterGenerator.Instance.ListedOnLeave.Remove(coll.transform.parent.gameObject);
        
        if (coll.transform.parent.gameObject)
        {
            Destroy(coll.transform.parent.gameObject);
        }
        Destroy(coll.gameObject);
        
    }



}