using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MapRotateController : MonoBehaviour
{
	[SerializeField] private GameObject map;
	[SerializeField] private GameObject player;
	// Update is called once per frame
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			map.transform.RotateAround(player.transform.position, Vector3.up, 90f);
		}
	}
}