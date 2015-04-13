using UnityEngine;
using System.Collections;
using System.IO;

public class ArrayScript : MonoBehaviour 
{
	private ArrayScript instance;

	private TerrainArray terrain;
	private TerrainArray[] storage;
	private string[] playerPath;

	private char wall = 'W';
	private char jump = 'J';
	private char slide = 'S';
	private char empty = 'O';

	private int arrayFillIndex = 32;
	private int currentBin = 0;
	private int arrayDirection = 1;

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
		storage = new TerrainArray[7];
		storage[0] = new TerrainArray(3);
		storage[1] = new TerrainArray(3);
		storage[2] = new TerrainArray(3);
		storage[3] = new TerrainArray(3);
		storage[4] = new TerrainArray(3);
		storage[5] = new TerrainArray(3);
		storage[6] = new TerrainArray(3);

		playerPath = new string[500];

		#region Bin 0
		// (X, X, O)
		//Option 1
		storage[0].SetData((int)LaneColumn.A, 0, empty);
		storage[0].SetData((int)LaneColumn.B, 0, wall);
		storage[0].SetData((int)LaneColumn.C, 0, empty);

		//Option 2
		storage[0].SetData((int)LaneColumn.A, 1, slide);
		storage[0].SetData((int)LaneColumn.B, 1, wall);
		storage[0].SetData((int)LaneColumn.C, 1, empty);

		//Option 3
		storage[0].SetData((int)LaneColumn.A, 2, wall);
		storage[0].SetData((int)LaneColumn.B, 2, wall);
		storage[0].SetData((int)LaneColumn.C, 2, jump);
		#endregion

		#region Bin 1
		//(X, O, O)
		//Option 1
		storage[1].SetData((int)LaneColumn.A, 0, wall);
		storage[1].SetData((int)LaneColumn.B, 0, empty);
		storage[1].SetData((int)LaneColumn.C, 0, empty);
		
		//Option 2
		storage[1].SetData((int)LaneColumn.A, 1, wall);
		storage[1].SetData((int)LaneColumn.B, 1, empty);
		storage[1].SetData((int)LaneColumn.C, 1, slide);
		
		//Option 3
		storage[1].SetData((int)LaneColumn.A, 2, wall);
		storage[1].SetData((int)LaneColumn.B, 2, jump);
		storage[1].SetData((int)LaneColumn.C, 2, empty);
		#endregion

		#region Bin 2
		//(X, O, O)
		//Option 1
		storage[2].SetData((int)LaneColumn.A, 0, wall);
		storage[2].SetData((int)LaneColumn.B, 0, empty);
		storage[2].SetData((int)LaneColumn.C, 0, jump);
		
		//Option 2
		storage[2].SetData((int)LaneColumn.A, 1, wall);
		storage[2].SetData((int)LaneColumn.B, 1, empty);
		storage[2].SetData((int)LaneColumn.C, 1, slide);
		
		//Option 3
		storage[2].SetData((int)LaneColumn.A, 2, wall);
		storage[2].SetData((int)LaneColumn.B, 2, slide);
		storage[2].SetData((int)LaneColumn.C, 2, jump);
		#endregion

		#region Bin 3
		//(X, O, X)
		//Option 1
		storage[3].SetData((int)LaneColumn.A, 0, jump);
		storage[3].SetData((int)LaneColumn.B, 0, empty);
		storage[3].SetData((int)LaneColumn.C, 0, slide);
		
		//Option 2
		storage[3].SetData((int)LaneColumn.A, 1, wall);
		storage[3].SetData((int)LaneColumn.B, 1, empty);
		storage[3].SetData((int)LaneColumn.C, 1, wall);
		
		//Option 3
		storage[3].SetData((int)LaneColumn.A, 2, wall);
		storage[3].SetData((int)LaneColumn.B, 2, slide);
		storage[3].SetData((int)LaneColumn.C, 2, wall);
		#endregion

		#region Bin 4
		//(O, O, X)
		//Option 1
		storage[4].SetData((int)LaneColumn.A, 0, slide);
		storage[4].SetData((int)LaneColumn.B, 0, empty);
		storage[4].SetData((int)LaneColumn.C, 0, wall);
		
		//Option 2
		storage[4].SetData((int)LaneColumn.A, 1, jump);
		storage[4].SetData((int)LaneColumn.B, 1, empty);
		storage[4].SetData((int)LaneColumn.C, 1, wall);
		
		//Option 3
		storage[4].SetData((int)LaneColumn.A, 2, slide);
		storage[4].SetData((int)LaneColumn.B, 2, jump);
		storage[4].SetData((int)LaneColumn.C, 2, wall);
		#endregion

		#region Bin 5
		//(O, O, X)
		//Option 1
		storage[5].SetData((int)LaneColumn.A, 0, empty);
		storage[5].SetData((int)LaneColumn.B, 0, empty);
		storage[5].SetData((int)LaneColumn.C, 0, wall);
		
		//Option 2
		storage[5].SetData((int)LaneColumn.A, 1, jump);
		storage[5].SetData((int)LaneColumn.B, 1, empty);
		storage[5].SetData((int)LaneColumn.C, 1, wall);
		
		//Option 3
		storage[5].SetData((int)LaneColumn.A, 2, empty);
		storage[5].SetData((int)LaneColumn.B, 2, slide);
		storage[5].SetData((int)LaneColumn.C, 2, wall);
		#endregion

		#region Bin 6
		//(O, X, X)
		//Option 1
		storage[6].SetData((int)LaneColumn.A, 0, empty);
		storage[6].SetData((int)LaneColumn.B, 0, wall);
		storage[6].SetData((int)LaneColumn.C, 0, empty);
		
		//Option 2
		storage[6].SetData((int)LaneColumn.A, 1, empty);
		storage[6].SetData((int)LaneColumn.B, 1, wall);
		storage[6].SetData((int)LaneColumn.C, 1, slide);
		
		//Option 3
		storage[6].SetData((int)LaneColumn.A, 2, jump);
		storage[6].SetData((int)LaneColumn.B, 2, wall);
		storage[6].SetData((int)LaneColumn.C, 2, wall);
		#endregion

		//Start adding at index 32 in the terrain array or later or it won't show up!
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(arrayFillIndex < 499)
		{
			arrayFillIndex++;
			if(arrayFillIndex % 5 == 0)
		{
			int select = Random.Range(0, storage[currentBin].Length);

			//Debug.Log("Index: " + arrayFillIndex + " Select: " + select);

			char laneOneValue = storage[currentBin].GetData((int)LaneColumn.A, select);
			char laneTwoValue = storage[currentBin].GetData((int)LaneColumn.B, select);
			char laneThreeValue = storage[currentBin].GetData((int)LaneColumn.C, select);

			terrain.SetData((int)LaneColumn.A, arrayFillIndex, laneOneValue);
			terrain.SetData((int)LaneColumn.B, arrayFillIndex, laneTwoValue);
			terrain.SetData((int)LaneColumn.C, arrayFillIndex, laneThreeValue);

			currentBin += arrayDirection;
			if(currentBin == 0 || currentBin == 6)
				arrayDirection *= -1;
		}
		}

		if(Input.GetKeyDown(KeyCode.L))
		{
			Debug.Log("Key Pressed");
			LogData();
		}
	}

	private void LogData()
	{
		Debug.Log("Logging Data");
		int logNumber = Random.Range(0, 10000);
		string path = "log" + logNumber.ToString() + ".tad";

		using(FileStream fs = File.Create(path))
		{
			StringWriter writer = new StringWriter();
			writer.Write("test outside loop");

			for(int i = 499; i >= 0; i--)
			{
				writer.Write("test inside loop");
				writer.WriteLine(terrain.GetData((int)LaneColumn.A, i) + terrain.GetData((int)LaneColumn.B, i) + terrain.GetData((int)LaneColumn.C, i));
			}


		}

		Debug.Log("Data logged to: " + path);
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