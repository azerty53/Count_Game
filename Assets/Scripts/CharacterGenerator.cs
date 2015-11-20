using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CharacterGenerator : MonoBehaviour {

    public List<GameObject> CharacterTypes;
    public Vector2 chPos;

    void Awake()
    {

    }

    void CreateCharacter()
    {
        GameObject createdGameObject = Instantiate(CharacterTypes[Random.Range(0, CharacterTypes.Count)]);
        
    }

    IEnumerator TimeToCreate()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
    }


}
