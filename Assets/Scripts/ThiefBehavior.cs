using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class ThiefBehavior : MonoBehaviour {

    public GameObject placeHolder;
    public GameObject coin;
    private GameObject coinContainer;
    private List<GameObject> coinsDropping = new List<GameObject>();
    private void OnEnable()
    {
        if (transform.GetComponent<Animator>().GetBool("Leaving"))
        {
            NewCoin();
        }

    }
    private void Start()
    {
        coinContainer=new GameObject();
        coinContainer.name = "Coin Container";
        coinContainer.transform.parent = transform.parent;
        
    }
    private void NewCoin()
    {

        GameObject theCoin = Instantiate(coin, placeHolder.transform.position, Quaternion.Euler(0, 45,Random.Range(0,360))) as GameObject;
        Rigidbody rigidbodyCoin = theCoin.GetComponent<Rigidbody>();

        rigidbodyCoin.AddForce(new Vector3(0, 5, 0),ForceMode.Impulse);
        StartCoroutine("CreateNewCoin");
        coinsDropping.Add(theCoin);

        if (coinsDropping.Count >= 3)
        {
            Destroy(coinsDropping[0]);
            coinsDropping.RemoveAt(0);
        }
        theCoin.transform.parent = coinContainer.transform;

    }

    IEnumerator CreateNewCoin()
    {
       yield return new WaitForSeconds(2.0f);
        NewCoin();
    }


}
