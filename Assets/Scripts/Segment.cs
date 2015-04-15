using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour 
{
	// Used to read from the Terrain Array
	public int index;

	// The column that the Path Segment is in, should be A, B, or C
	public ArrayScript.LaneColumn column;

	// The name of the row that the Path Segment is in, must match with Tag
	//public string rowName;

	// Holds all Path Segments that share a Tag
	public GameObject[] row;

	// Holds the script that contains the Terrain Array
	ArrayScript manager;

	// The number of Path Segment Rows
	private readonly int numberOfRows = 32;

	// The distance between Path Segment Rows
	private float distanceBetweenSegments = 2;

	// Use this for initialization
	void Start () 
	{
		// Find and save the script that holds the Terrain Array
		GameObject arrayManager = GameObject.FindGameObjectWithTag("Manager");
		manager = arrayManager.GetComponent<ArrayScript>();

		// Find and save all Path Segments in the appropriate row
		//row = GameObject.FindGameObjectsWithTag(rowName);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void JumpToNext()
	{
		// Move the Path Segment foward in Z based on the number of Path Segment Rows and the desired distance between Path Segments
		transform.position += new Vector3(0f, 0f, (numberOfRows * distanceBetweenSegments));

		// Give the Path Segment a new index based on it's new position
		index += numberOfRows;

		char temp = manager.Instance.Data.GetData((int)column, index);
		//Debug.Log("Column: " + column + " Index: " + index + " Char: " + temp);

		if(temp == 'O')
		{
			gameObject.tag = "Untagged";
			return;
		}

		if(temp == 'W')
		{
			//Debug.Log("Spawning a wall at " + column + index.ToString());
			gameObject.tag = "Wall";
			
			GameObject spawn;
			spawn = (GameObject)GameObject.Instantiate(Resources.Load("Wall"));
			spawn.transform.position = transform.position;
			return;
		}

		if(temp == 'J')
		{
			//Debug.Log("Spawning a jump at " + column + index.ToString());
			gameObject.tag = "FloorObstacle";
			
			GameObject spawn;
			spawn = (GameObject)GameObject.Instantiate(Resources.Load("Jump"));
			spawn.transform.position = transform.position;
			return;
		}

		if(temp == 'S')
		{
			//Debug.Log("Spawning a slide at " + column + index.ToString());
			gameObject.tag = "MiddleObstacle";
			
			GameObject spawn;
			spawn = (GameObject)GameObject.Instantiate(Resources.Load("Slide"));
			spawn.transform.position = transform.position;
			return;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Player")
		{
			//Debug.LogWarning("Jump");

			// Call the Jump To Next Function on every Path Segment in the same Row
			foreach(GameObject element in row)
			{
				Segment script;
				script = element.GetComponent<Segment>();

				script.JumpToNext();
			}
		}
	}
}
