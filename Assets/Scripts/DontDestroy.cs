using UnityEngine;
using System.Collections;
using System;
public class DontDestroy : MonoBehaviour {

    void Awake()
    {

        Func<GameObject[]> t = delegate { return GameObject.FindGameObjectsWithTag(transform.tag);  };
        if (t.Invoke().Length>1)
        {
            foreach(GameObject go in t.Invoke())
            {
                Destroy(go);
            }
            Instantiate(transform.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }

}
