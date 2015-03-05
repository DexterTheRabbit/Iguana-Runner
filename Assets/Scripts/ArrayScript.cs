using UnityEngine;
using System.Collections;

public class ArrayScript : MonoBehaviour 
{
	private ArrayScript instance;
	//private Random randomNumber;

	private TerrainArray terrain;
	private TerrainArray storage;

	private char wall = 'W';
	private char jump = 'J';
	private char slide = 'S';
	private char empty = 'O';

	private int arrayFillIndex = 32;

	public enum LaneColumn
	{
		A = 0,
		B = 1,
		C = 2
	}

	// Use this for initialization
	void Start () 
	{
		instance = this;
		//randomNumber = new Random();

		terrain = new TerrainArray(500);
		storage = new TerrainArray(3);

		//Saving to the storage array
		storage.SetData((int)LaneColumn.A, 0, wall);
		storage.SetData((int)LaneColumn.B, 0, empty);
		storage.SetData((int)LaneColumn.C, 0, empty);

		storage.SetData((int)LaneColumn.A, 1, empty);
		storage.SetData((int)LaneColumn.B, 1, wall);
		storage.SetData((int)LaneColumn.C, 1, empty);

		storage.SetData((int)LaneColumn.A, 2, empty);
		storage.SetData((int)LaneColumn.B, 2, empty);
		storage.SetData((int)LaneColumn.C, 2, wall);

		//Start adding at index 32 in the terrain array or later or it won't show up!
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(arrayFillIndex < 500)
			if(arrayFillIndex % 2 == 0)
		{
			int select = Random.Range(0, 3);

			//Debug.Log("Index: " + arrayFillIndex + " Select: " + select);

			char laneOneValue = storage.GetData((int)LaneColumn.A, select);
			char laneTwoValue = storage.GetData((int)LaneColumn.B, select);
			char laneThreeValue = storage.GetData((int)LaneColumn.C, select);

			terrain.SetData((int)LaneColumn.A, arrayFillIndex, laneOneValue);
			terrain.SetData((int)LaneColumn.B, arrayFillIndex, laneTwoValue);
			terrain.SetData((int)LaneColumn.C, arrayFillIndex, laneThreeValue);

			//arrayFillIndex++;
			Debug.Log(arrayFillIndex);
		}

		arrayFillIndex++;
	}

	public TerrainArray Data
	{
		get
		{
			return terrain;
		}
	}

	public ArrayScript Instance
	{
		get
		{
			return instance;
		}
	}
}
