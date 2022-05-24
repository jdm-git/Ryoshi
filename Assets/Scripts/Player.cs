using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
	public Joystick joystick;
	private void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		UpdateMotor(new Vector3(x, y, 0));
	}
}
