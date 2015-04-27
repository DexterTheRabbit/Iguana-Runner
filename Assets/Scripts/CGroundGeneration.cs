using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CGroundGeneration : MonoBehaviour 
{
    // A dictionary of the names of every ground section, along with their number as the key
    private Dictionary<int, string> _groundDictionary = new Dictionary<int,string>();
    
    // A list of spawned objects by key, along with a list of the objects themselves.
    private List<int> _spanwedObjects = new List<int>();
    private List<GameObject> _activeLaneSegments = new List<GameObject>();
    
    // SpawnPosition is where the next object will appear. positionIncrement is a vector3 that doesn't change.
    private Vector3 _spawnPosition;
    private Vector3 _positionIncrement = new Vector3(0f, 0f, 50f);
    
    // In the event that we move a previously spawned object instead of spawning a new one, this will hold the object while we move it.
    private GameObject _objectToMove;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("starting");
        
        for (int i = 1; i <= 17; i++)
        {
            // This will be concatonated with the iterator in the following for loop to create our dictionary of object names.
            string itemName = "Ground_Section_";
            itemName = itemName + i;

            _groundDictionary.Add(i, itemName);
            Debug.Log(itemName);
        }

        // This pre-spawns four objects. Objects that spawn after will spawn at the end of the fourth one.
        for (int i = 0; i <= 4; i++)
        {
            SpawnNext();
            
        }
	}

    /// <summary>
    /// Spawns a new ground segment or moves a previously used one from the _activeLaneSegments list
    /// </summary>
    public void SpawnNext()
    {
        // Choose a random int from 1 to 17 and use it as the key in our dictionary.
        int key = Random.Range(1, 18);
        // spawnNew is true by default. This may change later, depending on if it needs to or not.
        bool spawnNew = true;

        // Get the name of the object to spawn using our key.
        string objectName;
        _groundDictionary.TryGetValue(key, out objectName);

        //Check to see if we've spawned an object with this name before.
        foreach(GameObject laneSegment in _activeLaneSegments)
        {
            if (laneSegment.name == objectName && laneSegment.activeSelf == false || laneSegment.name == objectName + "(Clone)" && laneSegment.activeSelf == false)
            {
                spawnNew = false;
                _objectToMove = laneSegment;
            }
            else if (laneSegment.name == objectName && laneSegment.activeSelf == true || laneSegment.name == objectName + "(Clone)" && laneSegment.activeSelf == true)
            {
                spawnNew = true;
            }
        }

        if (spawnNew)
        {
            GameObject temp = (GameObject)Resources.Load(objectName);
            GameObject.Instantiate(temp);
            //Debug.Log(objectName);
            _activeLaneSegments.Add(temp);
            Debug.Log(temp);
            temp.transform.position = _spawnPosition;
        }
        else
        {
            _objectToMove.transform.position = _spawnPosition;
            _objectToMove.SetActive(true);
        }

        // Increment spawnPosition.
        _spawnPosition += _positionIncrement;
    }
	
	// Update is called once per frame
	void Update () 
    {
        // Unnecessary in this script.
	}
}
