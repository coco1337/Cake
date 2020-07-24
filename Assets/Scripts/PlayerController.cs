using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
	[SerializeField] private CharacterController characterController;
	[SerializeField] private float moveSpeed;
	
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		characterController.SimpleMove(new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, 0));
	}
}