using UnityEngine;
using System.Collections;

public class CBillboardSpawn : MonoBehaviour 
{
    public int nextPrefabZ = 10;

    GameObject cam;

	// Use this for initialization
	void Start () 
    {
        cam = GameObject.Find("Main Camera");

        StartCoroutine(SpawnLane());
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    IEnumerator SpawnLane()
    {
        while (cam.transform.position.z > -40f)
        {
            this.GetComponent<CBillboardPool>().Activate(Random.Range(0, this.GetComponent<CBillboardPool>().objectsToPool.Length), new Vector3(0f, -0.5f, nextPrefabZ * 50f), Quaternion.identity);
            nextPrefabZ++;

            yield return new WaitForSeconds(2.5f);
        }
    }
}
