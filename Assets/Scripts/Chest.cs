using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
	public Sprite emptyChest;
	public int ryoAmount = 5;
	protected override void OnCollect()
	{

		if (!collected)
		{
			collected = true;
			GetComponent<SpriteRenderer>().sprite = emptyChest;
			GameManager.instance.ShowText("+" + ryoAmount + " ryo!", 25, Color.white, transform.position, Vector3.up * 25, 1.5f);
			GameManager.instance.ryos += ryoAmount;
		}
	}
}
