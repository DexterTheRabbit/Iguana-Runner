using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGroundGeneration : MonoBehaviour 
{
    Dictionary<int, string> _groundDictionary = new Dictionary<int,string>();
    List<int> _spanwedObjects = new List<int>();
    List<GameObject> _activeLaneSegments = new List<GameObject>();
    Vector3 _spawnPosition;
    Vector3 _positionIncrement = new Vector3(0f, 0f, 50f);
    GameObject _objectToMove;

	// Use this for initialization
	void Start () 
    {
        string itemName = "ground_section_";

        for (int i = 1; i == 17; i++)
        {
            itemName = itemName + i.ToString();

            _groundDictionary.Add(i, itemName);
        }

        for (int i = 0; i == 4; i++)
        {
            SpawnNext(_spawnPosition);
        }
	}

    public void SpawnNext(Vector3 position)
    {
        int key = Random.Range(1, 18);
        bool spawnNew = true;

        string objectName;
        _groundDictionary.TryGetValue(key, out objectName);

        foreach(GameObject laneSegment in _activeLaneSegments)
        {
            if (laneSegment.name == objectName && laneSegment.activeSelf == false)
            {
                spawnNew = false;
                _objectToMove = laneSegment;
            }
            else if (laneSegment.name == objectName && laneSegment.activeSelf == true)
            {
                spawnNew = true;
            }
        }

        if (spawnNew)
        {
            GameObject temp = (GameObject)Resources.Load(objectName);
            _activeLaneSegments.Add(temp);
            temp.transform.position = position;
        }
        else
        {
            _objectToMove.transform.position = position;
            _objectToMove.SetActive(true);
        }

        _spawnPosition += _positionIncrement;
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
