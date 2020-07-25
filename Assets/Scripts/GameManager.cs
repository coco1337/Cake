using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
    [SerializeField] private int mLevel;
	[SerializeField] private GaugeController gaugeController;
    [SerializeField] private Map mMap;

    private Player player;
	public Player GetPlayer => player;
	public GaugeController GetGaugeController => gaugeController;

	private void Start()
	{
		instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Init();
	}

    public void Init()
    {
        mMap.Init();
    }

    public void ClearStage()
    {
        string nextScene = string.Format("Stage{0}", mLevel + 1);
        LoadScene(nextScene);
    }

    void LoadScene(string name)
    {
        LoadScene(name);
    }
}