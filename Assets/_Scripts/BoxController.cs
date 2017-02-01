using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
	public Vector2 currentTile;


	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	public void PlayerMove (string dir, Vector2 targetTile)
	{
		if (targetTile != currentTile)
			return;

		Move (dir);
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
			"time", PlayerController.self.moveDuration
		));

		//check against hole tile after moving
		StartCoroutine (CheckHole (PlayerController.self.moveDuration));
	}

	void Score ()
	{
		//TODO: fall animation
		GameController.self.Score ();
		Destroy (gameObject);
	}

	IEnumerator CheckHole (float wait)
	{
		yield return new WaitForSeconds (wait);

		if (currentTile == GameController.self.currentHole.currentTile)
			Score ();
	}
}
