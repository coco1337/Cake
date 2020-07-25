using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class EndSceneEvents : MonoBehaviour
{
	[SerializeField] private float rotateSpeed;
	[SerializeField] private List<GameObject> childList;
	[SerializeField] private float moveSpeed;
	private List<Vector3> childOriginPos = new List<Vector3>();

	private void Start()
	{
		foreach (var child in childList)
		{
			childOriginPos.Add(child.transform.localPosition);
			child.transform.localPosition *= 20;
		}

		StartCoroutine(StrawberryCycle());
	}

	private void Update()
	{
		this.transform.Rotate(this.transform.forward, rotateSpeed, Space.World);
	}

	private IEnumerator StrawberryCycle()
	{
		int i = 0;
		while (i < SlicedStrawberryCounter.instance.GetStrawberryCount)
		{
			childList[i].transform.localPosition = Vector3.Lerp(childList[i].transform.localPosition, childOriginPos[i], moveSpeed * Time.fixedDeltaTime);
			yield return null;

			if (Vector3.Distance(childList[i].transform.localPosition, childOriginPos[i]) < 0.1f)
			{
				childList[i].transform.localPosition = childOriginPos[i];
				++i;
			}
		}
	}
}