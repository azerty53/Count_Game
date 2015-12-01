using UnityEngine;
using System;
using UnityEngine.UI;
public class NumberInput : MonoBehaviour {

    InputField numberInputField;
    InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
    int submittedText;


    void Awake()
    {
        numberInputField = GetComponent<InputField>();
        submitEvent.AddListener(OnSubmit);
        numberInputField.onEndEdit = submitEvent;
    }

    
    void OnSubmit(string arg0)
    {
        submittedText = Int16.Parse(numberInputField.text);
        GameManager.Instance.CompareValue(submittedText);

    }


}
