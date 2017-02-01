using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public static GridController self;

	void Awake ()
	{
		self = this;
	}

	public Vector3 GetWorldPosition (Vector2 tile)
	{
		float xPos = tile.x * Global.tileSize;
		float yPos = tile.y * Global.tileSize;
		return new Vector3 (xPos, yPos, 0f);
	}
}
