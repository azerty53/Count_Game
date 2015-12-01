using UnityEngine;
using System.Collections;

public class ObjectGeneratorPooling : MonoBehaviour {

    private static ObjectGeneratorPooling instance;
    protected ObjectGeneratorPooling() { }
    public static ObjectGeneratorPooling Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType(typeof(ObjectGeneratorPooling)) as ObjectGeneratorPooling;
            return instance;
        }

    }






}
