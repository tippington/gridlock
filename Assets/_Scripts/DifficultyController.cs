using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
	public static DifficultyController self;
	public float grade = 0f;

	//config
	public float easyEnemySpawnInterval;
	public float hardEnemySpawnInterval;
	public float easyHotCycleInterval;
	public float hardHotCycleInterval;
	public float easyHotPercentage;
	public float hardHotPercentage;

	void Awake ()
	{
		self = this;
	}

	void Update ()
	{
		grade = Mathf.Clamp ((1f / 100f) * (Mathf.Pow (Global.currentScore, 2f)), 0f, 100f);
		Global.enemySpawnInterval = Mathf.Lerp (easyEnemySpawnInterval, hardEnemySpawnInterval, grade / 100f);
		Global.hotPercentage = Mathf.Lerp (easyHotPercentage, hardHotPercentage, grade / 100f);
		Global.hotCycleInterval = Mathf.Lerp (easyHotCycleInterval, hardHotCycleInterval, grade / 100f);
	}
}
