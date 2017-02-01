using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
	public Vector2 currentTile;

	public void Score ()
	{
		Destroy (gameObject);
	}
}
