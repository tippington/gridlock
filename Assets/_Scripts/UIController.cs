using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

public class UIController : MonoBehaviour
{
	public static UIController self;

	public UIElement eMainMenu;
	public UIElement eInGameHud;

	public Text pointsScore;
	public Text coinsScore;


	void Awake ()
	{
		self = this;
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		pointsScore.text = Global.currentScore.ToString ();
		if (Global.coins == 1)
			coinsScore.text = Global.coins.ToString () + " <size=16>star</size>";
		else
			coinsScore.text = Global.coins.ToString () + " <size=16>stars</size>";
	}

	public void GameOver ()
	{
		eInGameHud.Hide (false);
		eMainMenu.Show (false);
	}

	public void OnGameEvent (string gameEvent)
	{
		switch (gameEvent) {
		}
	}

	public void OnButtonClick (string buttonName)
	{
		switch (buttonName) {
		}
	}
}
