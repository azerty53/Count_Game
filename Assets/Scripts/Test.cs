using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Test : MonoBehaviour {

    private float [] myList = { 1, 50.0f, 20.0f,0.0f };

    void Start()

    {
        var enumerableList = from element in myList
                           orderby element
                           select element;

        List<float> SortedList = enumerableList.ToList();

    }




}
