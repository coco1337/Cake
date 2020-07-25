using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RotateSelf : MonoBehaviour
{
	[SerializeField] private float rotateSpeed;
	
	private void Update()
	{
		this.transform.Rotate(Vector3.up * rotateSpeed);		
	}
}