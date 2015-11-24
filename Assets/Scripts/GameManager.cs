using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    protected GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
    }

    public enum PlayState { Play, Stop, Pause };
    public PlayState gameState;
    public int stepsTime;
    public string buttonText="2X";

    void Awake()
    {
        OnStart();
    }

    public void OnStart()
    {
        gameState = PlayState.Play;
    }

    public void SpeedTime(float speed)
    {
        if (Time.timeScale < stepsTime * speed)
        {
            Time.timeScale *= speed;

            int temp = (int)(Time.timeScale * speed);
            buttonText = temp.ToString()+"X";

            if (Time.timeScale == stepsTime * speed)
            {
                buttonText = "Normal Speed";

            }
        }

        else
        {
            Time.timeScale = 1.0f;
            buttonText = ((int)speed).ToString() + "X";

        }
        
    }

    public void CompareValue(int numberCounted)
    {
        if (numberCounted == HouseBehaviour.Instance.In)
        {
            Debug.Log("You win");
        }

        else
        {
            Debug.Log("You Lose");
            
        }
        foreach (GameObject guests in CharacterGenerator.Instance.ListedGuest)
        {
            Instantiate(guests, Vector3.zero, Quaternion.identity);
        }
    }
}
