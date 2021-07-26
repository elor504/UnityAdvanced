using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemSlot : MonoBehaviour
{
	public Image itemImage;
	public TextMeshProUGUI itemName;
	public TextMeshProUGUI itemLevel;
	public TextMeshProUGUI itemDamage;
	public TextMeshProUGUI itemCost;

	public int weaponIndex;
	
	public void InitSlot(Sprite imageSprite,string name,string level,string damage,int index,string currentCost)
	{
		itemImage.sprite = imageSprite;
		itemName.text = name;
		itemLevel.text = "Level: " + level;
		itemDamage.text = "Damage: " + damage;
		weaponIndex = index;
		itemCost.text = "Cost: " + currentCost;
	}

	void UpdateItemInfo(Weapon weaponinfo)
	{
		itemName.text = weaponinfo.WeaponName;
		itemLevel.text = "Level: " + weaponinfo.weaponLevel.ToString();
		itemDamage.text = "Damage: " + weaponinfo.currentDamage.ToString();
		itemCost.text = "Cost: " + weaponinfo.currentBuy.ToString();
	}

	public void OnClickItemSlot()
	{
		if (CanBuy())
		{
			GameManager.getInstance.playerData.playerWeapons[weaponIndex].weaponLevel += 1;
			UpdateItemInfo(GameManager.getInstance.playerData.playerWeapons[weaponIndex]);
			UIManager.getInstance.UpdatePlayerDamageInUI();
		}
		else
		{
			Debug.Log("Not enough money");
		}
	}

	bool CanBuy()
	{
		if(GameManager.getInstance.playerData.playerCurrency >= GameManager.getInstance.playerData.playerWeapons[weaponIndex].currentBuy)
		{
			Debug.Log("sword amount: " + GameManager.getInstance.playerData.playerWeapons[weaponIndex].currentBuy);
			GameManager.getInstance.playerData.playerCurrency -= GameManager.getInstance.playerData.playerWeapons[weaponIndex].currentBuy;
			UIManager.getInstance.ShowPlayerCurrency();
			
			return true;
		}
		return false;
	}


}
