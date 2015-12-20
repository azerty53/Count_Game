using UnityEngine;
using System;

public class Boundaries : MonoBehaviour
{

    public void OnTriggerEnter (Collider coll)
    {
        Destroy(coll.gameObject);
    }



}