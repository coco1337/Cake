using System.Collections;
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

	public void IncreaseStrawberryCount()
	{
		++strawberryCount;
		if (strawberryCount <= gaugeCount)
		{
			gauges[gaugeCount - strawberryCount].color = Color.black;
		}
	}

	public void UseSugarHighMode()
	{
		
	}
}