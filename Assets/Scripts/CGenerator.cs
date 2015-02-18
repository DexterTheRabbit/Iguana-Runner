using UnityEngine;
using System.Collections;

public class CGenerator : MonoBehaviour 
{
	public GameObject[] _EasyTerrainSetPieces;
	public GameObject[] _MediumTerrainSetPieces;
	public GameObject[] _HardTerrainSetPieces;
	//public GameObject[] terrainObjects;
	public QASetPieces testSetPieces;
	public bool sendDebugMessages;

	public enum QASetPieces
	{
		Easy,
		Medium,
		Hard,
		None
	}

	private int[] tablePoints;
	private int tablePointsMin;
	private int tablePointsMax;
	private int forceSetPiece = 0;

	private int SetPiecesCount;
	private int longestSetArrayLength;
	private string currentSetPiece;
	private GameObject previousSetPiece = null;

	private float spawnX = 0;
	private float spawnY = 0;
	private float spawnZ = 30f;
	private float OffsetZ = 60f;

	private Rect setPieceRect;
	private Rect setPieceSpawnRect;
	private Rect setPieceForce;

	// Use this for initialization
	void Start () 
	{
		tablePointsMin = 0;
		tablePointsMax = 100 + 1;

		tablePoints = new int[2];
		tablePoints[0] = 33;
		tablePoints[1] = 66;

		setPieceRect = new Rect(Screen.width * .02f, 20f, 125f, 30f);
		setPieceSpawnRect = new Rect(Screen.width * .40f, 20f, 125f, 30f);
		setPieceForce = new Rect(Screen.width * .80f, 20f, 125, 30f);

		longestSetArrayLength = _EasyTerrainSetPieces.Length;

		if(_MediumTerrainSetPieces.Length > longestSetArrayLength)
			longestSetArrayLength = _MediumTerrainSetPieces.Length;

		if(_HardTerrainSetPieces.Length > longestSetArrayLength)
			longestSetArrayLength = _HardTerrainSetPieces.Length;

		int choseSetPiece = Random.Range(0, _EasyTerrainSetPieces.Length - 1);
		Vector3 position = new Vector3(spawnX, spawnY, spawnZ);
		GameObject.Instantiate(_EasyTerrainSetPieces[choseSetPiece], position, Quaternion.identity);

		if(sendDebugMessages)
			Debug.Log("Spawned Easy Set Piece #" + (choseSetPiece + 1));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
			testSetPieces = QASetPieces.Easy;

		if(Input.GetKeyDown(KeyCode.Alpha2))
			testSetPieces = QASetPieces.Medium;

		if(Input.GetKeyDown(KeyCode.Alpha3))
			testSetPieces = QASetPieces.Hard;

		if(Input.GetKeyDown(KeyCode.Alpha4))
			testSetPieces = QASetPieces.None;

		if(Input.GetKeyDown(KeyCode.LeftArrow) && forceSetPiece > 0)
			forceSetPiece--;

		if(Input.GetKeyDown(KeyCode.RightArrow) && forceSetPiece < longestSetArrayLength)
			forceSetPiece++;

		if(Input.GetKeyDown(KeyCode.Space))
			Application.LoadLevel("SceneTest");
	}

	void SpawnEasySetPiece()
	{
		int choseSetPiece = Random.Range(0, _EasyTerrainSetPieces.Length - 1);

		if(forceSetPiece > 0 && forceSetPiece <= _EasyTerrainSetPieces.Length)
			choseSetPiece = forceSetPiece - 1;

		else if(forceSetPiece > _EasyTerrainSetPieces.Length)
			choseSetPiece = _EasyTerrainSetPieces.Length - 1;

		spawnZ += OffsetZ;
		Vector3 position = new Vector3(spawnX, spawnY, spawnZ);

		previousSetPiece = (GameObject)GameObject.Instantiate(_EasyTerrainSetPieces[choseSetPiece], position, Quaternion.identity);
		if(sendDebugMessages)
			Debug.Log("Spawned Easy Set Piece #" + (choseSetPiece + 1));
	}

	void SpawnMediumSetPiece()
	{
		int choseSetPiece = Random.Range(0, _MediumTerrainSetPieces.Length - 1);

		if(forceSetPiece > 0 && forceSetPiece <= _MediumTerrainSetPieces.Length)
			choseSetPiece = forceSetPiece - 1;

		else if(forceSetPiece > _MediumTerrainSetPieces.Length)
			choseSetPiece = _MediumTerrainSetPieces.Length - 1;

		spawnZ += OffsetZ;
		Vector3 position = new Vector3(spawnX, spawnY, spawnZ);

		previousSetPiece = (GameObject)GameObject.Instantiate(_MediumTerrainSetPieces[choseSetPiece], position, Quaternion.identity);
		if(sendDebugMessages)
			Debug.Log("Spawned Medium Set Piece #" + (choseSetPiece + 1));
	}

	void SpawnHardSetPiece()
	{
		int choseSetPiece = Random.Range(0, _HardTerrainSetPieces.Length - 1);

		if(forceSetPiece > 0 && forceSetPiece <= _HardTerrainSetPieces.Length)
			choseSetPiece = forceSetPiece - 1;

		else if(forceSetPiece > _HardTerrainSetPieces.Length)
			choseSetPiece = _HardTerrainSetPieces.Length - 1;

		spawnZ += OffsetZ;
		Vector3 position = new Vector3(spawnX, spawnY, spawnZ);

		previousSetPiece = (GameObject)GameObject.Instantiate(_HardTerrainSetPieces[choseSetPiece], position, Quaternion.identity);
		if(sendDebugMessages)
			Debug.Log("Spawned Hard Set Piece #" + (choseSetPiece + 1));
	}

	void OnTriggerEnter(Collider element)
	{
		if(element.gameObject.tag == "SetPiece")
		{
			currentSetPiece = element.gameObject.name;
			int choseDifficulty;

			if(testSetPieces == QASetPieces.Easy)
				choseDifficulty = 1;

			else if(testSetPieces == QASetPieces.Medium)
				choseDifficulty = 50;

			else if(testSetPieces == QASetPieces.Hard)
				choseDifficulty = 99;

			else
				choseDifficulty = Random.Range(tablePointsMin, tablePointsMax);

			if(choseDifficulty <= tablePoints[0])
				SpawnEasySetPiece();

			if(choseDifficulty > tablePoints[0] && choseDifficulty <= tablePoints[1])
				SpawnMediumSetPiece();

			if(choseDifficulty > tablePoints[1])
				SpawnHardSetPiece();

			SetPiecesCount++;
		}
	}

	void OnGUI()
	{
		GUI.Label(setPieceRect, currentSetPiece);
		GUI.Label(setPieceSpawnRect, testSetPieces.ToString());
		GUI.Label(setPieceForce, forceSetPiece.ToString());
	}
}
