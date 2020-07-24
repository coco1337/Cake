﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Strawberry : MonoBehaviour
{
	private GaugeController gaugeController;

	private void Start()
	{
		gaugeController = FindObjectOfType<GaugeController>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			gaugeController.IncreaseStrawberryCount();
			// TODO : 이펙트 구현
			Destroy(this.gameObject);
		}
	}
}