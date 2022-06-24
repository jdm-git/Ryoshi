using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text fields
    public Text levelText, hitpointText, upgradeCostText, ryosText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
	{
		if (right)
		{
            currentCharacterSelection++;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
		}
		else
		{
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
	}
    private void OnSelectionChanged()
	{
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
	}

    // Weapon upgrade
    public void OnUpgradeClick()
	{
		if (GameManager.instance.TryUpgradeWeapon())
		{
            UpdateMenu();
		}
	}

    //Update character information

    public void UpdateMenu()
	{
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        upgradeCostText.text = "Not ready yet";

        //Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        ryosText.text = GameManager.instance.ryos.ToString();
        levelText.text = "Not ready yet";

        //Xp bar
        xpText.text = "Not ready yet";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
	}
}
