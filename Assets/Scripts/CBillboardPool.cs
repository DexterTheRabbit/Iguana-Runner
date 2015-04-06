using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CBillboardPool : MonoBehaviour 
{
    public GameObject[] objectsToPool;
    public int[] poolAmount;

    public List<GameObject>[] pool;

	// Use this for initialization
	void Start ()
    {
        ObjInstantiate();
	}

    void ObjInstantiate()
    {
        GameObject temp;

        pool = new List<GameObject>[objectsToPool.Length];

        for(int i = 0; i < objectsToPool.Length; i++)
        {
            pool[i] = new List<GameObject>();

            for(int x = 0; x < poolAmount[i]; x++)
            {
                temp = (GameObject)Instantiate(objectsToPool[i]);
                temp.transform.parent = this.transform;

                pool[i].Add(temp);
            }
        }
    }

    public GameObject Activate(int id, Vector3 position, Quaternion rotation)
    {
        for(int i = 0; i < pool[id].Count; i++)
        {
            if(!pool[id][i].activeSelf)
            {
                pool[id][i].SetActive(true);
                pool[id][i].transform.position = position;
                pool[id][i].transform.rotation = rotation;
                return pool[id][i];
            }
        }

        pool[id].Add((GameObject)Instantiate(objectsToPool[id]));
        pool[id][pool[id].Count - 1].transform.position = position;
        pool[id][pool[id].Count - 1].transform.rotation = rotation;

        return pool[id][pool[id].Count - 1];
    }

    public void Disable(GameObject obj)
    {
        obj.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
