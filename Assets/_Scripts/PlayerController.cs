using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController self;
	public bool canMove = false;
	public float moveDuration = 0.2f;
	public Vector2 currentTile;

	private Vector3 targetPos;

	void Awake ()
	{
		self = this;
	}

	void Start ()
	{
		Global.currentPlayer = gameObject;
	}

	void Update ()
	{
		if (!canMove)
			return;

		if (Input.GetKeyUp ("up"))
			Move ("up");
		if (Input.GetKeyUp ("down"))
			Move ("down");
		if (Input.GetKeyUp ("left"))
			Move ("left");
		if (Input.GetKeyUp ("right"))
			Move ("right");			
	}

	void OnCollisionEnter (Collision col)
	{
		switch (col.gameObject.tag) {
		case "Enemy":
			Die ();
			col.gameObject.GetComponent<EnemyController> ().Die ();
			break;
		case "Coin":
			GameController.self.GotCoin ();
			col.gameObject.GetComponent<CoinController> ().Die ();
			break;
		}
	}

	public void GameStart ()
	{
		currentTile = Vector2.zero;
		canMove = true;
	}

	public void Die ()
	{
		Destroy (gameObject);
	}

	void Move (string dir)
	{
		if (!CheckCanMove (dir))
			return;
		
		switch (dir) {
		case "up":
			currentTile = new Vector2 (currentTile.x, currentTile.y + 1);
			break;
		case "down":
			currentTile = new Vector2 (currentTile.x, currentTile.y - 1);
			break;
		case "left":
			currentTile = new Vector2 (currentTile.x - 1, currentTile.y);
			break;
		case "right":
			currentTile = new Vector2 (currentTile.x + 1, currentTile.y);
			break;
		}

		iTween.MoveTo (gameObject, iTween.Hash (
			"position", GridController.self.GetWorldPosition (currentTile),
			"islocal", false,
			"looptype", "none",
			"easetype", iTween.EaseType.easeOutQuad,
			"time", moveDuration		
		));

		foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box")) {
			BoxController bc = box.GetComponent<BoxController> ();
			bc.PlayerMove (dir, currentTile);
		}

		CheckDie ();
	}

	void CheckDie ()
	{
//		if (currentTile == GameController.self.currentHole.currentTile)
//			Die ();
	}

	bool CheckCanMove (string dir)
	{
		bool canMove = true;

		//check boundaries
		int gridSizeX = (int)GameController.self.gridSize.x;
		int gridSizeY = (int)GameController.self.gridSize.y;
		//check against hole
		Vector2 holeTile = GameController.self.currentHole.currentTile;
		//check against box
		List<Vector2> boxTiles = new List<Vector2> ();
		foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box"))
			boxTiles.Add (box.GetComponent<BoxController> ().currentTile);

		switch (dir) {
		case "up":
			if (currentTile.y > 5)
				canMove = false;
			if (currentTile.x == holeTile.x && currentTile.y + 1 == holeTile.y)
				canMove = false;
			for (int i = 0; i < boxTiles.Count; i++) {
				if (currentTile.x == boxTiles [i].x) {
					if (currentTile.y == boxTiles [i].y - 1) {
						if (gridSizeY == 3) {
							if (boxTiles [i].y == 1)
								canMove = false;
						} else if (gridSizeY == 5) {
							if (boxTiles [i].y == 3)
								canMove = false;
						}
					}
				}
			}
			break;
		case "down":
			if (currentTile.y < -5)
				canMove = false;
			if (currentTile.x == holeTile.x && currentTile.y - 1 == holeTile.y)
				canMove = false;
			for (int i = 0; i < boxTiles.Count; i++) {
				if (currentTile.x == boxTiles [i].x) {
					if (currentTile.y == boxTiles [i].y + 1) {
						if (gridSizeY == 3) {
							if (boxTiles [i].y == -1)
								canMove = false;
						} else if (gridSizeY == 5) {
							if (boxTiles [i].y == -3)
								canMove = false;
						}
					}
				}
			}
			break;
		case "left":
			if (currentTile.x < -2)
				canMove = false;
			if (currentTile.y == holeTile.y && currentTile.x == holeTile.x + 1)
				canMove = false;
			for (int i = 0; i < boxTiles.Count; i++) {
				if (currentTile.y == boxTiles [i].y) {
					if (currentTile.x == boxTiles [i].x + 1) {
						if (gridSizeX == 3) {
							if (boxTiles [i].x == -1)
								canMove = false;
						} else if (gridSizeX == 5) {
							if (boxTiles [i].x == -3)
								canMove = false;
						}
					}
				}
			}
			break;
		case "right":
			if (currentTile.x > 2)
				canMove = false;
			if (currentTile.y == holeTile.y && currentTile.x == holeTile.x - 1)
				canMove = false;
			for (int i = 0; i < boxTiles.Count; i++) {
				if (currentTile.y == boxTiles [i].y) {
					if (currentTile.x == boxTiles [i].x - 1) {
						if (gridSizeX == 3) {
							if (boxTiles [i].x == 1)
								canMove = false;
						} else if (gridSizeX == 5) {
							if (boxTiles [i].x == 3)
								canMove = false;
						}
					}
				}
			}
			break;			
		}
		return canMove;
	}
}
