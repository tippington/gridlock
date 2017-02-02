using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
	public static SpawnController self;

	public bool spawnEnemies = false;
	public bool spawnCoins = false;
	public List<GameObject> enemies;
	public List<GameObject> coins;
	public float coinSpawnInterval = 6f;

	private List<Vector3> enemySpawnPoints;
	private List<Vector3> coinSpawnPoints;
	private float enemySpawnTimer;
	private float coinSpawnTimer;

	void Awake ()
	{
		self = this;
	}

	void Update ()
	{
		if (spawnEnemies) {
			enemySpawnTimer += Time.deltaTime;
			if (enemySpawnTimer >= DifficultyController.self.enemySpawnInterval) {
				enemySpawnTimer = 0f;
				SpawnEnemy ();
			}
		}
		if (spawnCoins) {
			coinSpawnTimer += Time.deltaTime;
			if (coinSpawnTimer >= coinSpawnInterval) {
				coinSpawnTimer = 0f;
				SpawnCoin ();
			}
		}
	}

	public void GameStart ()
	{
		enemySpawnTimer = 0f;
		coinSpawnTimer = 0f;
		enemySpawnPoints = new List<Vector3> ();
		coinSpawnPoints = new List<Vector3> ();
		if (GameController.self.gridSize.x == 3f) {
			//left 0-2
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, 1f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, 0f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, -1f)));
			//right 3-5
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, 1f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, 0f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, -1f)));
			//top 6-8
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-1f, 10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (0f, 10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (1f, 10f)));
			//bottom 9-11
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-1f, -10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (0f, -10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (1f, -10f)));
		} else if (GameController.self.gridSize.x == 5f) {
			//left 0-4
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, 2f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, 1f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, 0f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, -1f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-6f, -2f)));
			//right 5-9
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, 2f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, 1f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, 0f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, -1f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (6f, -2f)));
			//top 10-14
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-2f, 10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-1f, 10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (0f, 10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (1f, 10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (2f, 10f)));
			//bottom 15-19
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-2f, -10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (-1f, -10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (0f, -10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (1f, -10f)));
			enemySpawnPoints.Add (GridController.self.GetWorldPosition (new Vector2 (2f, -10f)));
		}

		coinSpawnPoints = enemySpawnPoints;
	}

	void SpawnEnemy ()
	{
		//pick enemy to spawn
		GameObject enemy = enemies [Random.Range (0, enemies.Count)];
		//pick spawn point
		int index = Random.Range (0, enemySpawnPoints.Count);
		Vector3 spawnPoint = enemySpawnPoints [index];
		//spawn enemy
		GameObject newEnemy = (GameObject)Instantiate (enemy, spawnPoint, Quaternion.identity);
		//set enemy direction
		EnemyController ec = newEnemy.GetComponent<EnemyController> ();
		if (GameController.self.gridSize.x == 3f) {
			if (index < 3)
				ec.SetDirection ("right");
			else if (index < 6)
				ec.SetDirection ("left");
			else if (index < 9)
				ec.SetDirection ("down");
			else
				ec.SetDirection ("up");
		} else if (GameController.self.gridSize.x == 5f) {
			if (index < 5)
				ec.SetDirection ("right");
			else if (index < 10)
				ec.SetDirection ("left");
			else if (index < 15)
				ec.SetDirection ("down");
			else
				ec.SetDirection ("up");
		}
	}

	void SpawnCoin ()
	{
		//pick coin to spawn
		GameObject coin = coins [Random.Range (0, coins.Count)];
		//pick spawn point
		int index = Random.Range (0, coinSpawnPoints.Count);
		Vector3 spawnPoint = coinSpawnPoints [index];
		//spawn enemy
		GameObject newCoin = (GameObject)Instantiate (coin, spawnPoint, Quaternion.identity);
		//set enemy direction
		CoinController cc = newCoin.GetComponent<CoinController> ();
		if (GameController.self.gridSize.x == 3f) {
			if (index < 3)
				cc.SetDirection ("right");
			else if (index < 6)
				cc.SetDirection ("left");
			else if (index < 9)
				cc.SetDirection ("down");
			else
				cc.SetDirection ("up");
		} else if (GameController.self.gridSize.x == 5f) {
			if (index < 5)
				cc.SetDirection ("right");
			else if (index < 10)
				cc.SetDirection ("left");
			else if (index < 15)
				cc.SetDirection ("down");
			else
				cc.SetDirection ("up");
		}
	}


}
