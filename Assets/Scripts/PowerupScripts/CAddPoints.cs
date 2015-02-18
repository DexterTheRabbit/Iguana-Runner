using UnityEngine;
using System.Collections;

public class CAddPoints : MonoBehaviour {

    public int PointsToAdd;
    GameObject player;
    CPlayer playerScript;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<CPlayer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider collided)
    {
        playerScript._Score += 100f;
    }
}
