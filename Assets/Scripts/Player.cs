using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
	public Joystick joystick;
	private void FixedUpdate()
	{
		float x = joystick.Horizontal;
		float y = joystick.Vertical;

		UpdateMotor(new Vector3(x, y, 0));
	}
}
