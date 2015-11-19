using UnityEngine;
using System.Collections;

public class CharacterBehavior : MonoBehaviour
{

	public int sens;
	public float speed;
    private Vector2 posXY;

    void Awake()
    {
        posXY = new Vector2(transform.position.x, transform.position.y);
    }

	private void Move ()
	{
        posXY.x += sens * speed;
        transform.position = new Vector2(posXY.x, transform.position.y);
	}

    void Update()
    {
        Move();

    }


}
