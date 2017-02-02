using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
	public static DifficultyController self;
	public float grade = 0f;
	public float enemySpawnInterval;

	//config
	public float easyEnemySpawnInterval = 1.6f;
	public float hardEnemySpawnInterval = 0.52f;

	void Awake ()
	{
		self = this;
	}

	void Update ()
	{
		grade = Mathf.Clamp ((1f / 100f) * (Mathf.Pow (Global.currentScore, 2f)), 0f, 100f);
		enemySpawnInterval = Mathf.Lerp (easyEnemySpawnInterval, hardEnemySpawnInterval, grade / 100f);
	}
}
