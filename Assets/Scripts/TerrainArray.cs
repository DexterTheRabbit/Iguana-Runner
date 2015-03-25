using UnityEngine;
using System.Collections;

public class TerrainArray 
{
	char[] data;

	int totalLaneIndex;
	string output;

	public TerrainArray(int laneIndex)
	{
		SetIndex(laneIndex);
		data = new char[totalLaneIndex * 3];

		for(int i = 0; i < data.Length; i++)
		{
			data[i] = 'O';
		}

		//Debug.Log(totalLaneIndex);
	}

	public char GetData(int column, int index)
	{
		return data[index + (column * totalLaneIndex)];
	}

	public void SetData(int column, int index, char value)
	{
		data[index + (column * totalLaneIndex)] = value;
	}

	public override string ToString ()
	{
		foreach(char element in data)
		{
			output += element;
		}

		return output;
	}

	public int Length
	{
		get
		{
			return totalLaneIndex;
		}
	}

	private void SetIndex(int index)
	{
		totalLaneIndex = index;
	}
}
