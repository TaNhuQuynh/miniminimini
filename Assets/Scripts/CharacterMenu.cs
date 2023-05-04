using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //text fields
    public Text levelText, hitPointText, pesosText, upgradeCostText, xpText;

    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //if we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprite.Count)
                currentCharacterSelection = 0;

            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;

            //if we went too far away
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprite.Count - 1;

            OnSelectionChange();
        }
    }

    public void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprite[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //update the character infor
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrice.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrice[GameManager.instance.weapon.weaponLevel].ToString();

        //meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointText.text = GameManager.instance.player.hitPonit.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        //xpBar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.exp.ToString() + " total exp points";//display total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int preLevelXp = GameManager.instance.GetXpLevel(currLevel - 1);
            int currLevelXp= GameManager.instance.GetXpLevel(currLevel);

            int diff = currLevelXp - preLevelXp;
            int currIntoLevel = GameManager.instance.exp - preLevelXp;

            float completionRatio = (float)currIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currIntoLevel.ToString() + " / " + diff;
        }

        
    }
}
