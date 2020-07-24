﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GaugeController : MonoBehaviour
{
	[SerializeField] private int gaugeCount;
	[SerializeField] private List<Image> gauges;
	[SerializeField] private Image gaugePrefab;
	[SerializeField] private GameObject gaugeGroup;

	private int strawberryCount;
	
	// Start is called before the first frame update
	private void Start()
	{
		for (int i = 0; i < gaugeCount; ++i)
		{
			var gauge = Instantiate(gaugePrefab, gaugeGroup.transform);
			gauges.Add(gauge);
		}
	}

	private IEnumerator SugarHighMode(int i)
	{
		for (int j = 0; gaugeCount - j > 0; ++j)
		{
			var inner = gauges[gaugeCount - j - 1].GetComponentsInChildren<Image>()[1];
			while (inner.fillAmount > 0)
			{
				inner.fillAmount -= Time.fixedDeltaTime / 2.5f;
				yield return null;
			}
		}
	}

	public void IncreaseStrawberryCount()
	{
		++strawberryCount;
		if (strawberryCount <= gaugeCount)
		{
			gauges[gaugeCount - strawberryCount].GetComponentsInChildren<Image>()[1].fillAmount = 1;
		}
	}

	public void UseSugarHighMode()
	{
		for (int i = 0; gaugeCount - i - 1 > 0; ++i)
		{
			if (gauges[gaugeCount - i - 1].GetComponentsInChildren<Image>()[1].fillAmount != 0)
			{
				StartCoroutine(SugarHighMode(i));
				return;
			}
		}
	}
}