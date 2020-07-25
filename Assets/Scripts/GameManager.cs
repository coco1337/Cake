using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	[SerializeField] private Player player;
	[SerializeField] private GaugeController gaugeController;

	public Player GetPlayer => player;
	public GaugeController GetGaugeController => gaugeController;

	private void Start()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
}