using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController self;

	public GameObject box;
	public GameObject hole;
	public Vector2 gridSize;
	public HoleController currentHole;

	private int xMin;
	private int xMax;
	private int yMin;
	private int yMax;

	void Awake ()
	{
		self = this;
	}

	void Start ()
	{
		//TODO: temporary
		GameStart ();
	}

	public void GameStart ()
	{
		InitGrid ();
		Global.currentScore = 0;
		PlayerController.self.GameStart ();
		SpawnController.self.GameStart ();
		SpawnBox ();
	}

	public void GameOver ()
	{
		
	}

	void Update ()
	{
		
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (0, 0, 150, 20), "Score : " + Global.currentScore.ToString ());
		GUI.Label (new Rect (0, 20, 150, 20), "Coins : " + Global.coins.ToString ());
		if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height - 60, 120, 60), "Reinit")) {
			Application.LoadLevel ("Level0");
		}
	}

	public void Score ()
	{
		Global.currentScore++;
		currentHole.Score ();
		SpawnBox ();
	}

	public void GotCoin ()
	{
		Global.coins++;
	}

	void InitGrid ()
	{
		//get x boundaries
		if (gridSize.x == 3f) {
			xMin = -1;
			xMax = 1;
		} else if (gridSize.x == 5f) {
			xMin = -2;
			xMax = 2;
		}
		//get y boundaries
		if (gridSize.y == 3f) {
			yMin = -1;
			yMax = 1;
		} else if (gridSize.y == 5f) {
			yMin = -2;
			yMax = 2;
		}
	}

	void SpawnBox ()
	{
		Vector2 avoid = PlayerController.self.currentTile;
		List<int> columns = new List<int> ();
		List<int> rows = new List<int> ();

		//add all potential tiles to list
		for (int i = xMin; i <= xMax; i++) {
			if (i != avoid.x)
				columns.Add (i);
		}
		for (int j = yMin; j <= yMax; j++) {
			if (j != avoid.y)
				rows.Add (j);
		}

		int tileX = columns [Random.Range (0, columns.Count)];
		int tileY = rows [Random.Range (0, rows.Count)];

		Vector2 targetTile = new Vector2 (tileX, tileY);
		Vector3 targetPos = GridController.self.GetWorldPosition (targetTile);

		GameObject newBox = (GameObject)Instantiate (box, targetPos, Quaternion.identity);
		newBox.GetComponent<BoxController> ().currentTile = targetTile;

		SpawnHole (targetTile);
	}

	void SpawnHole (Vector2 boxTile)
	{
		Vector2 avoid = PlayerController.self.currentTile;
		List<int> columns = new List<int> ();
		List<int> rows = new List<int> ();

		//add all potential tiles to list
		for (int i = xMin; i <= xMax; i++) {
			if (i != avoid.x && i != boxTile.x)
				columns.Add (i);
		}
		for (int j = yMin; j <= yMax; j++) {
			if (j != avoid.y && j != boxTile.y)
				rows.Add (j);
		}

		int tileX = columns [Random.Range (0, columns.Count)];
		int tileY = rows [Random.Range (0, rows.Count)];

		Vector2 targetTile = new Vector2 (tileX, tileY);
		Vector3 targetPos = GridController.self.GetWorldPosition (targetTile);

		GameObject newHole = (GameObject)Instantiate (hole, targetPos, Quaternion.identity);
		currentHole = newHole.GetComponent<HoleController> ();
		currentHole.currentTile = targetTile;
	}
}
