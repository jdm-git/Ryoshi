using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Collidable
{
	public string sceneName;

	protected override void OnCollide(Collider2D coll)
	{
		if(coll.name == "Player")
		{
			// Player enters the house
			GameManager.instance.SaveState();
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

		}
	}
}
