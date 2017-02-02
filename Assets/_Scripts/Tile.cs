using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Vector2 location;
	public bool isOnGrid = false;
	public bool isHot = false;
	public float hotDuration;
	public float warningDuration;
	public float size;

	private GameObject tileObj;
	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		hotDuration = Global.hotCycleInterval * 0.6f;
		warningDuration = Global.hotCycleInterval * 0.2f;
	}

	public void LineUp ()
	{
		Vector3 pos = GridController.self.GetWorldPosition (location);
		transform.position = pos;
	}

	public void HeatUp ()
	{
		StartCoroutine (Heat ());
	}

	IEnumerator Heat ()
	{
		animator.SetTrigger ("startOpen");
		isHot = true;
		yield return new WaitForSeconds (hotDuration);
		isHot = false;
		animator.SetTrigger ("startClose");
	}


}
