using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class LobbyManager : MonoBehaviour
{
	private bool pressed;
	[SerializeField] private float fadeMul;
	[SerializeField] private CanvasScaler canvasScaler;
	[SerializeField] private Image fadeAlpha;

    private void Start()
    {
		SoundManager.Inst.Play("title_bgm",true,0.2f);
    }

    private void Update()
	{
		if (Input.anyKey && !pressed)
		{
			pressed = true;
			GoToFirstStage();
		}
	}

	private void GoToFirstStage()
	{
		StartCoroutine(ControlCanvasScale());
		
	}

	private IEnumerator ControlCanvasScale()
	{
		var p = 33f;
		while (p > 5)
		{
			p -= Time.fixedDeltaTime * fadeMul;
			canvasScaler.scaleFactor = p;

			yield return null;
		}

		var q = 0f;
		while (q <= 1)
		{
			q += Time.fixedDeltaTime * 2;
			fadeAlpha.color = new Color(0, 0, 0, q);
			yield return null;
		}
		SoundManager.Inst.Stop("title_bgm");
		SoundManager.Inst.Play("default_bgm", true,0.2f);
		SceneManager.LoadScene("Stage1");
	}
}