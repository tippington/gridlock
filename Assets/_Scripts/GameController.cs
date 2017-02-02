using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController self;

	public enum GameState
	{
		MENU,
		PLAYING}

	;

	public GameState state;
	public bool showDebug = false;

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
		state = GameState.MENU;
	}

	public void GameStart ()
	{
		ResetBoard ();
		InitGrid ();
		Global.currentScore = 0;
		PlayerController.self.GameStart ();
		SpawnController.self.GameStart ();
		GridController.self.GameStart ();
		SpawnBox ();
		state = GameState.PLAYING;
	}

	public void GameOver ()
	{
		UIController.self.GameOver ();
		state = GameState.MENU;
	}

	void Update ()
	{
		
	}

	void OnGUI ()
	{
		if (!showDebug)
			return;
		
		GUI.Label (new Rect (0, 0, 150, 20), "Score : " + Global.currentScore.ToString ());
		GUI.Label (new Rect (0, 20, 150, 20), "Coins : " + Global.coins.ToString ());
		GUI.Label (new Rect (0, 60, 300, 20), "Enemy Spawn Interval : " + Global.enemySpawnInterval.ToString ("F4"));
		GUI.Label (new Rect (0, 80, 300, 20), "Hot Percentage : " + Global.hotPercentage.ToString ("F4"));
		GUI.Label (new Rect (0, 100, 300, 20), "Hot Cycle Interval : " + Global.hotCycleInterval.ToString ("F4"));
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

	void ResetBoard ()
	{
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
			Destroy (enemy);
		foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box"))
			Destroy (box);
		foreach (GameObject hole in GameObject.FindGameObjectsWithTag("Hole"))
			Destroy (hole);
		foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
			Destroy (coin);
		foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
			Destroy (tile);
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
