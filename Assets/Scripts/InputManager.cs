using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {


    private static InputManager _instance;

    protected InputManager() { }

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(InputManager)) as InputManager;
            }
            return _instance;
        }
    }

    public List<Text> display;
    private int indexList;
    public void DisplayNewText(int inputValue)
    {
        if (display.Count > indexList)
        {
            display[indexList].text = inputValue.ToString();
            indexList++;
        }
        else { Debug.Log("All Space used, delete input to proceed"); }

    }

    public void DeleteLastEntry()
    {
        if (indexList > 0)
        {
            indexList--;
            display[indexList].text = "";
        }
       else { Debug.Log("Last entry already deleted"); }

    }


}
