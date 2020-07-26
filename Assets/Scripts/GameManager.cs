using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField] private int mLevel;
	[SerializeField] private GaugeController gaugeController;
	[SerializeField] private Map mMap;
	[SerializeField] private float fadeMul;

	[SerializeField] private CanvasScaler canvasScaler;
	[SerializeField] private Image fadeAlpha;

	private Player player;
	private bool isUpdateStrawberryCount = false;
	
	public Player GetPlayer => player;
	public GaugeController GetGaugeController => gaugeController;

	private void Start()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		StartCoroutine(SceneStart());

		Init();
	}

	private void Init()
	{
		mMap.Init();
	}

	public void ClearStage()
	{
		string nextScene = string.Format("Stage{0}", mLevel + 1);
		GoNextScene(nextScene);
	}

	private void GoNextScene(string sceneName)
	{
		if (isUpdateStrawberryCount)
			return;

		isUpdateStrawberryCount = true;
		SlicedStrawberryCounter.instance.AddStrawberryCount(gaugeController.GetStrawberryCount);
		StartCoroutine(ControlCanvasScale(sceneName));
	}

	private IEnumerator ControlCanvasScale(string sceneName)
	{
		var p = 33f;
		while (p > 5)
		{
			p -= Time.fixedDeltaTime * fadeMul;
			canvasScaler.scaleFactor = p;

			yield return null;
		}

		yield return new WaitForSeconds(1f);
		
		/*
		var q = 0f;
		while (q <= 1)
		{
			q += Time.fixedDeltaTime * 2;
			fadeAlpha.color = new Color(0, 0, 0, q);
			yield return null;
		}
		*/
		
		SceneManager.LoadScene(sceneName);
	}

	private IEnumerator SceneStart()
	{
		var p = 5f;
		while (p < 33)
		{
			p += Time.fixedDeltaTime * fadeMul;
			canvasScaler.scaleFactor = p;
			yield return null;
		}
	}
}