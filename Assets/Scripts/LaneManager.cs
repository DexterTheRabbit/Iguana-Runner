using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System;

public class LaneManager : MonoBehaviour {

    System.Collections.Generic.List<GameObject> lanes;
    public GameObject player;
    public GameObject center;
    public GameObject left;
    public GameObject right;

	// Use this for initialization
	void Start () 
    {
        lanes = new System.Collections.Generic.List<GameObject>();

        lanes.Add((GameObject)GameObject.Instantiate(left));
        lanes.Add((GameObject)GameObject.Instantiate(center));
        lanes.Add((GameObject)GameObject.Instantiate(right));

        

        for (int i = 0; i < 3; i++)
        {
            lanes[i].transform.position = new Vector3(lanes[i].transform.position.x, lanes[i].transform.position.y, 0f);
        }

        lanes.Add((GameObject)GameObject.Instantiate(left));
        lanes.Add((GameObject)GameObject.Instantiate(center));
        lanes.Add((GameObject)GameObject.Instantiate(right));
        for(int i = 3; i <6; i++)
        {
            lanes[i].transform.position = new Vector3(lanes[i].transform.position.x, lanes[i].transform.position.y, 60f);
        }

        lanes.Add((GameObject)GameObject.Instantiate(left));
        lanes.Add((GameObject)GameObject.Instantiate(center));
        lanes.Add((GameObject)GameObject.Instantiate(right));
        for (int i = 6; i < 9; i++)
        {
            lanes[i].transform.position = new Vector3(lanes[i].transform.position.x, lanes[i].transform.position.y, 120f);
        }
            
	}
	
	// Update is called once per frame
	void Update () 
    {
	    foreach(GameObject laneObject in lanes)
        {
            if(player.transform.position.z - laneObject.transform.position.z>60)
            {
                laneObject.transform.position = new Vector3(laneObject.transform.position.x, laneObject.transform.position.y, player.transform.position.z + 119f);
            }
        }
	}
}
