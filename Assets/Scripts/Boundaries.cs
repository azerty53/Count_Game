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
        Destroy(coll.gameObject);
        
    }



}