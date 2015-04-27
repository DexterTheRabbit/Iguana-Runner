using UnityEngine;
using System.Collections;

public class CGroundSegment : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}

    void OnTriggerEnter(Collider objectCollidedWith)
    {
        if(objectCollidedWith.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
