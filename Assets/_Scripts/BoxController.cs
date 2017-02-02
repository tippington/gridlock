using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
	public Vector2 currentTile;
	public float rotationRange;

	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
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
			"easetype", iTween.EaseType.easeOutSine,
			"time", PlayerController.self.moveDuration
		));

		float angle = Random.Range (-rotationRange, rotationRange);

		transform.Rotate (Vector3.forward * angle);
		animator.SetTrigger ("startPush");

		//check against hole tile after moving
		StartCoroutine (CheckHole (PlayerController.self.moveDuration));
	}

	void Score ()
	{
		animator.SetTrigger ("startSolve");
		GameController.self.Score ();
		StartCoroutine (DieAfter (0.18f));
	}

	IEnumerator CheckHole (float wait)
	{
		yield return new WaitForSeconds (wait);

		if (currentTile == GameController.self.currentHole.currentTile)
			Score ();
	}

	IEnumerator DieAfter (float wait)
	{
		yield return new WaitForSeconds (wait);
		Destroy (gameObject);
	}
}
