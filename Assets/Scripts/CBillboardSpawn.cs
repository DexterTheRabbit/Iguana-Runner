using UnityEngine;
using System.Collections;

public class CBillboardSpawn : MonoBehaviour 
{
    int num = 0;

    GameObject cam;

	// Use this for initialization
	void Start () 
    {
        cam = GameObject.Find("Main Camera");

        this.GetComponent<CBillboardPool>().Activate(0, new Vector3(0f, -0.5f, 0), Quaternion.identity);
        this.GetComponent<CBillboardPool>().Activate(0, new Vector3(0f, -0.5f, 50), Quaternion.identity);
        this.GetComponent<CBillboardPool>().Activate(0, new Vector3(0f, -0.5f, 100), Quaternion.identity);
        this.GetComponent<CBillboardPool>().Activate(0, new Vector3(0f, -0.5f, 150), Quaternion.identity);

        StartCoroutine(SpawnLane());
	}
	
	// Update is called once per frame
	void Update () 
    {
        //num += 1 * Time.deltaTime;

        //Activate(0, new Vector3(0f, 0f, num * 5), Quaternion.identity);

        //for (int i = 0; i < 50; i++)
        //{
        //    this.GetComponent<CBillboardPool>().Activate(0, new Vector3(0f, 0f, i * 50), Quaternion.identity);
        //}
	}

    IEnumerator SpawnLane()
    {
        while(cam.transform.position.z > -40)
        {
            this.GetComponent<CBillboardPool>().Activate(0, new Vector3(0f, -0.5f, num * 50), Quaternion.identity);
            num++;

            yield return new WaitForSeconds(1.7f);
        }
    }
}
