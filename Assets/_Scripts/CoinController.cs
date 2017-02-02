using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT}

	;

	public Direction direction;

	public float moveSpeed = 3f;
	public float moveSpeedRange = 0f;
	public float lifespan = 8f;

	private float age = 0f;

	void Update ()
	{
		age += Time.deltaTime;
		if (age >= lifespan)
			Die ();
	}

	public void SetDirection (string dir)
	{
		switch (dir) {
		case "up":
			direction = Direction.UP;
			break;
		case "down":
			direction = Direction.DOWN;
			break;
		case "left":
			direction = Direction.LEFT;
			break;
		case "right":
			direction = Direction.RIGHT;
			break;
		}
		StartMoving ();
	}

	void StartMoving ()
	{
		moveSpeed += Random.Range (-moveSpeedRange, moveSpeedRange);

		switch (direction) {
		case Direction.UP:
			GetComponent<Rigidbody> ().velocity = new Vector3 (0f, moveSpeed, 0f);
			break;
		case Direction.DOWN:
			GetComponent<Rigidbody> ().velocity = new Vector3 (0f, -moveSpeed, 0f);
			break;
		case Direction.LEFT:
			GetComponent<Rigidbody> ().velocity = new Vector3 (-moveSpeed, 0f, 0f);
			break;
		case Direction.RIGHT:
			GetComponent<Rigidbody> ().velocity = new Vector3 (moveSpeed, 0f, 0f);
			break;
		}	
	}

	public void Die ()
	{
		Destroy (gameObject);
	}
}
