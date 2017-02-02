using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
	public Vector2 currentTile;
	public float torqueRange;
	public GameObject solveFx;

	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	public void Score ()
	{
		float torque = Random.Range (-torqueRange, torqueRange);

		animator.SetTrigger ("startUnlock");
		iTween.PunchRotation (gameObject, iTween.Hash (
			"x", 0,
			"y", 0,
			"z", torque,
			"islocal", true,
			"looptype", "none",
			"time", 0.38f	
		));

		StartCoroutine (DieAfter (0.6f));
	}

	IEnumerator DieAfter (float wait)
	{
		GameObject fx = (GameObject)Instantiate (solveFx, transform.position, Quaternion.identity);
		fx.transform.SetParent (transform);
		yield return new WaitForSeconds (wait);
		Destroy (gameObject);
	}
}
