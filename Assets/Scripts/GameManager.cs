using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	[SerializeField] private Player player;

	public Player GetPlayer => player;

	private void Start()
	{
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    public void LoadMap(int level)
    {
        Map map = null; // 어떻게 맵 관리할지 미정
        map.Init();

    }
}