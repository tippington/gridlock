using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	public static GridController self;
	public int totalColumns = 7;
	public int totalRows = 13;

	public GameObject tileObj;
	public List<Tile> allTiles;
	public List<Tile> coldTiles;
	public List<Tile> hotTiles;

	private bool tilesReady = false;
	private float hotTimer;

	void Awake ()
	{
		self = this;
	}

	void Update ()
	{
		if (!tilesReady)
			return;
		
		hotTimer += Time.deltaTime;
		if (hotTimer >= Global.hotCycleInterval) {
			hotTimer = 0f;
			CycleHotTiles ();
		}
	}

	public void GameStart ()
	{
		InitTiles ();
	}

	public Vector3 GetWorldPosition (Vector2 tile)
	{
		float xPos = tile.x * Global.tileSize;
		float yPos = tile.y * Global.tileSize;
		return new Vector3 (xPos, yPos, 0f);
	}

	public bool CheckIfOnHotTile (Vector2 playerTile)
	{
		bool killPlayer = false;
		for (int i = 0; i < allTiles.Count; i++) {
			Vector2 tilePos = allTiles [i].location;
			if (playerTile == tilePos) {
				if (allTiles [i].isHot)
					killPlayer = true;
			}
		}
		return killPlayer;
	}

	void InitTiles ()
	{
		hotTimer = 0f;
		allTiles = new List<Tile> ();
		coldTiles = new List<Tile> ();
		hotTiles = new List<Tile> ();
		int columnRange = (totalColumns - 1) / 2;
		int rowRange = (totalRows - 1) / 2;

		for (int i = -columnRange; i <= columnRange; i++) {
			for (int j = -rowRange; j <= rowRange; j++) {
				//create tile
				GameObject newTileObj = (GameObject)Instantiate (tileObj, Vector3.zero, Quaternion.identity);
				//make child of grid
				newTileObj.transform.SetParent (transform);
				Tile newTile = newTileObj.GetComponent<Tile> ();
				//assign location
				newTile.location = new Vector2 (i, j);
				//assign isOnGrid
				newTile.isOnGrid = CheckOnGrid (i, j);
				//assign size
				newTile.size = Screen.width / (float)totalColumns;
				//line up
				newTile.LineUp ();
				allTiles.Add (newTile);

				if (!newTile.isOnGrid) {
					coldTiles.Add (newTile);
				}
			}
		}

		tilesReady = true;
	}

	void CycleHotTiles ()
	{
		List<Tile> tilesToHeat = new List<Tile> ();
		float hotPercentage = Global.hotPercentage;
		//select random tiles to heat
		for (int i = 0; i < coldTiles.Count; i++) {
			//roll dice for heat
			if (Random.Range (0f, 1f) < hotPercentage) {
				//remove that tile from coldTiles and add to tilesToHeat
				Tile hotTile = coldTiles [i];
				coldTiles.Remove (hotTile);
				tilesToHeat.Add (hotTile);
				//heat that tile
				hotTile.HeatUp ();
			}
		}
		//re-add hot tiles to coldTiles
		for (int j = 0; j < hotTiles.Count; j++) {
			coldTiles.Add (hotTiles [j]);
			hotTiles.Remove (hotTiles [j]);
		}
		//add all of tilesToHeat to hotTiles
		for (int k = 0; k < tilesToHeat.Count; k++) {
			hotTiles.Add (tilesToHeat [k]);
		}
	}

	bool CheckOnGrid (int x, int y)
	{
		bool onGrid = false;
		int sizeX = (int)GameController.self.gridSize.x;
		int sizeY = (int)GameController.self.gridSize.y;

		if (sizeX == 3) {
			if (x >= -1 && x <= 1) {
				if (sizeY == 3) {
					if (y >= -1 && y <= 1)
						onGrid = true;
				} else if (sizeY == 5) {
					if (y >= -2 && y <= 2)
						onGrid = true;
				}
			}
		} else if (sizeX == 5) {
			if (x >= -2 && x <= 2) {
				if (sizeY == 3) {
					if (y >= -1 && y <= 1)
						onGrid = true;
				} else if (sizeY == 5) {
					if (y >= -2 && y <= 2)
						onGrid = true;
				}
			}
		}
		return onGrid;
	}
}
