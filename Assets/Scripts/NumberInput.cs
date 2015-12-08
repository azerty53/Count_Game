using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class NumberInput : MonoBehaviour {

    InputField numberInputField;
    InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
    int submittedText;
    string textInput;

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

    void Update()
    {
        textInput = numberInputField.text;
        textInput = Regex.Replace(textInput, @"[^0-9 ]", "");
        numberInputField.text= textInput;

    }


}
