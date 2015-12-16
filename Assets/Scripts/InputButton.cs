using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour {

    public int buttonValue;
    public bool validation;
    public bool delete;
    private string answer;
    public void PressButton()
    {
        Debug.Log("button pushed");
        if (validation)
        {
            foreach (Text number in InputManager.Instance.display)
            {
                answer += number.text;

            }
            GameManager.Instance.CompareValue(int.Parse(answer));
        }

        else if (delete)
        {
            InputManager.Instance.DeleteLastEntry();
        }


        else { InputManager.Instance.DisplayNewText(buttonValue); }

    }



}
