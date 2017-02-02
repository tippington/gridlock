﻿using System.Collections;
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

	private float maxAlpha = 1f;
	private bool fadingIn = false;
	private bool fadingOut = false;
	private float fadeTimer;
	private GameObject tileObj;
	private Animator animator;
	private SpriteRenderer rend;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		rend = GetComponent<SpriteRenderer> ();
	}

	void Update ()
	{
		hotDuration = Global.hotCycleInterval * 0.6f;
		warningDuration = Global.hotCycleInterval * 0.2f;
//
//		if (rend == null)
//			return;
//
//		if (fadingIn || fadingOut) {
//			fadeTimer += Time.deltaTime;
//			float progress = fadeTimer / warningDuration;
//			//lerp color
//			Color newColor = Color.white;
//			if (fadingIn)
//				newColor.a = Mathf.Lerp (0f, maxAlpha, progress);
//			else
//				newColor.a = Mathf.Lerp (maxAlpha, 0f, progress);
//			rend.color = newColor;
//		}
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
