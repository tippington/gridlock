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
		if (currentTile == GameController.self.currentHole.currentTile)
			Die ();
	}
}
