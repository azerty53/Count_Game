using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpeedButton : MonoBehaviour {
    Text speedButtonText;
    

    void Awake()
    {
        speedButtonText = this.GetComponent<Text>();
    }

    void Update()

    {
        speedButtonText.text = GameManager.Instance.buttonText;
    }
   




}
