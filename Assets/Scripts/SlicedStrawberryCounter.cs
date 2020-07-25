using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SlicedStrawberryCounter : MonoBehaviour
{
	public static SlicedStrawberryCounter instance;
	private int strawberryCount;

	public void AddStrawberryCount(int i) => strawberryCount += i;
	public int GetStrawberryCount => strawberryCount;
	
	// Start is called before the first frame update
	private void Start()
	{
		instance = this;
		DontDestroyOnLoad(this);
	}
}