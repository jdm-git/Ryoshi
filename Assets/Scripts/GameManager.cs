using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	private void Awake()
	{
		if(GameManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		SceneManager.sceneLoaded += LoadState;
		DontDestroyOnLoad(gameObject);
	}

	// Resources 
	public List<Sprite> playerSprites;
	public List<Sprite> weaponSprites;
	public List<int> weaponPrices;
	public List<int> xpTable;

	// References
	public Player player;


	// Progress (xp, money)
	public int ryos;
	public int expierience;


	// Save the game state
	public void SaveState()
	{
		string s = "";

		s += "0" + "|";
		s += ryos.ToString() + "|";
		s += expierience.ToString() + "|";
		s += "0";

		PlayerPrefs.SetString("SaveState", s);
	}
	public void LoadState(Scene s, LoadSceneMode mode)
	{
		if (!PlayerPrefs.HasKey("SaveState"))
		{
			return;
		}

		string[] data = PlayerPrefs.GetString("SaveState").Split('|');

		// Change player skin

		ryos = int.Parse(data[1]);
		expierience = int.Parse(data[2]);
		// Change weapon lvl


		Debug.Log("LoadState");
	}
}
